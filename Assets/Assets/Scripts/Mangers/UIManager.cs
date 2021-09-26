using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EntitiesDatabaseObject entitiesDatabase;
    [Space]
    [SerializeField] private TMP_InputField resCountInputField;
    [SerializeField] private TMP_Dropdown resourcesDropDown;
    [Space]
    [SerializeField] private UIBlockFullInfoView blockInfo;
    [SerializeField] private UICalcResultView calcResultPanel;
    [SerializeField] private UIRecipesView recipesView;


    private int resourceCount = 0;
    private int selectedResourceIndex = 0;
    private int selectedRecipeIndex = 0;
    private List<BlockDataObj> recipes;
    private ResourcesCalculator resourcesCalculator;


    private void Awake()
    {
        entitiesDatabase.Init();
        Init(entitiesDatabase.Resources);

        recipesView.OnItemChanged += OnRecipeItemChanged;
        resCountInputField.onValueChanged.AddListener(OnResourceValueChanged);
        resourcesDropDown.onValueChanged.AddListener(OnResourceTypeChanged);

        resourcesCalculator = new ResourcesCalculator(entitiesDatabase);
        
        blockInfo.gameObject.SetActive(true);
        recipesView.gameObject.SetActive(true);
        calcResultPanel.gameObject.SetActive(false);
    }

    private void Start()
    {
        OnResourceTypeChanged(0);
    }


    public void Init(ResourceDataObj[] resourcesList)
    {
        resourcesDropDown.ClearOptions();

        var optionsList = new List<TMP_Dropdown.OptionData>();
        foreach (ResourceDataObj resDataObj in resourcesList)
        {
            optionsList.Add(new TMP_Dropdown.OptionData(resDataObj.Name, resDataObj.Sprite));
        }

        resourcesDropDown.AddOptions(optionsList);
    }

    private void CalculateRecipes()
    {
        var calcResult = resourcesCalculator.Calculate(recipes[selectedRecipeIndex], resourceCount);

        if (calcResult.recipeBlockData != null)
        {
            calcResultPanel.gameObject.SetActive(true);
            calcResultPanel.Init(calcResult);
        }
    }

    private void OnRecipeItemChanged(int index)
    {
        selectedRecipeIndex = index;
        blockInfo.Init(recipes[selectedRecipeIndex]);
        CalculateRecipes();
    }

    private void OnResourceValueChanged(string inputString)
    {
        if (int.TryParse(inputString, out int count))
        {
            resourceCount = count;
            CalculateRecipes();
        }
    }

    private void OnResourceTypeChanged(int selectedIndex)
    {
        selectedResourceIndex = selectedIndex;

        recipes = entitiesDatabase.GetRecipes(selectedResourceIndex);

        if (recipes != null && recipes.Count > 0)
        {
            blockInfo.gameObject.SetActive(true);
            recipesView.gameObject.SetActive(true);

            recipesView.Init(recipes);

            OnRecipeItemChanged(0);
        }
        else
        {
            blockInfo.gameObject.SetActive(false);
            recipesView.gameObject.SetActive(false);
            Debug.LogWarning($"No recipes found for {entitiesDatabase.Resources[selectedResourceIndex].Name} resource");
        }
    }

}
