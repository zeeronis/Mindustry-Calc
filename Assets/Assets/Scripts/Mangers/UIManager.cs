using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EntitiesDatabaseObject entitiesDB;
    [Space]
    //[SerializeField] private TMP_InputField resCountInputField;
    //[SerializeField] private TMP_Dropdown resourcesDropDown;
    //[SerializeField] private TMP_Dropdown calcTypeDropDown;
    [SerializeField] private UICalcSettingsInput calcSettingsInput;
    [Space]
    [SerializeField] private UIBlockFullInfoView blockInfo;
    [SerializeField] private UICalcResultView calcResultPanel;
    [SerializeField] private UIRecipesView recipesView;
    [SerializeField] private UITotalRawResources totalRawResources;


    private int selectedRecipeIndex = 0;
    private string selectedResourceName;

    private List<BlockDataObj> availableRecipes;
    private ResourcesCalculator calculator;


    private void Awake()
    {
        entitiesDB.Init();
        calculator = new ResourcesCalculator(entitiesDB);

        recipesView.OnItemChanged += OnRecipeItemChanged;
        calcSettingsInput.OnSettingsChanged += CalcSettingsInput_OnSettingsChanged;

        blockInfo.gameObject.SetActive(true);
        recipesView.gameObject.SetActive(true);
        calcResultPanel.gameObject.SetActive(false);
    }

    private void Start()
    {
        calcSettingsInput.Init(entitiesDB.Resources);
    }

    private bool CanCalculateRecipe()
    {
        if (selectedRecipeIndex < availableRecipes.Count &&
            calcSettingsInput.ItemsCount >= 0)
            return true;

        return false;
    }

    private void CalcTotalResources(Dictionary<ResourceDataObj, float> totalList, in CalculationResult calculationResult)
    {
        foreach (var itemStack in calculationResult.inputResources)
        {
            var resourceData = itemStack.resourceData;
            if (resourceData.IsRawResource)
            {
                if (!totalList.ContainsKey(resourceData))
                    totalList[resourceData] = 0;

                totalList[resourceData] += itemStack.count;
            }
            else
            {
                var recipe = entitiesDB.GetRecipes(resourceData.EntityName)[0];
                var calcResult = calculator.CalculateByOutputResourceCount(recipe, itemStack.count);

                CalcTotalResources(totalList, in calcResult);
            }
        }
    }

    private void CalculateRecipe()
    {
        if (!CanCalculateRecipe())
            return;

        CalculationResult calcResult = calcSettingsInput.CalcType == CalculationType.Resources
            ? calculator.CalculateByOutputResourceCount(availableRecipes[selectedRecipeIndex], calcSettingsInput.ItemsCount)
            : calculator.CalculateByBlocksCount(availableRecipes[selectedRecipeIndex], (int)calcSettingsInput.ItemsCount);

        if (calcResult.recipeBlockData != null)
        {
            // test 
            if (calcResult.inputResources?.Length > 0)
            {
                var totalResources = new Dictionary<ResourceDataObj, float>();
                CalcTotalResources(totalResources, in calcResult);

                var rawResourcesList = new List<ResourceStack>();
                foreach (var item in totalResources)
                {
                    rawResourcesList.Add(new ResourceStack(
                        item.Key, item.Value));
                }

                totalRawResources.Init(rawResourcesList.ToArray());
            }
            // end test

            calcResultPanel.gameObject.SetActive(true);
            calcResultPanel.Init(calcResult);
        }
    }

    private void UpdateRecipesList(string resourceName)
    {
        availableRecipes = entitiesDB.GetRecipes(resourceName);

        if (availableRecipes != null && availableRecipes.Count > 0)
        {
            blockInfo.gameObject.SetActive(true);
            recipesView.gameObject.SetActive(true);

            recipesView.Init(availableRecipes);
        }
        else
        {
            blockInfo.gameObject.SetActive(false);
            recipesView.gameObject.SetActive(false);
            Debug.LogWarning($"No recipes found for {resourceName} resource");
        }
    }

    private void OnRecipeItemChanged(int index)
    {
        selectedRecipeIndex = index;

        blockInfo.Init(availableRecipes[selectedRecipeIndex]);
        CalculateRecipe();
    }


    private void CalcSettingsInput_OnSettingsChanged()
    {
        var resourceName = calcSettingsInput.SelectedResourceName;
        if (selectedResourceName != resourceName)
        {
            selectedResourceName = resourceName;
            UpdateRecipesList(resourceName);
        }

        CalculateRecipe();
    }
}
