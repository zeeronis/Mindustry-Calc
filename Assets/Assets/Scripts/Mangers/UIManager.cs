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
    [SerializeField] private TMP_Dropdown calcTypeDropDown;
    [Space]
    [SerializeField] private UIBlockFullInfoView blockInfo;
    [SerializeField] private UICalcResultView calcResultPanel;
    [SerializeField] private UIRecipesView recipesView;


    private float itemsCount = 0;
    private int selectedResourceIndex = 0;
    private int selectedRecipeIndex = 0;
    private CalculationType calculationType;

    private List<BlockDataObj> recipes;
    private ResourcesCalculator resourcesCalculator;


    private void Awake()
    {
        entitiesDatabase.Init();
        Init(entitiesDatabase.Resources);

        recipesView.OnItemChanged += OnRecipeItemChanged;
        resCountInputField.onValueChanged.AddListener(OnResourceValueChanged);
        resourcesDropDown.onValueChanged.AddListener(OnResourceTypeChanged);
        calcTypeDropDown.onValueChanged.AddListener(OnCalculationTypeChanged);

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
        calcTypeDropDown.ClearOptions();


        var optionsList = new List<TMP_Dropdown.OptionData>();
        foreach (ResourceDataObj resDataObj in resourcesList)
        {
            optionsList.Add(new TMP_Dropdown.OptionData(resDataObj.Name, resDataObj.Sprite));
        }

        resourcesDropDown.AddOptions(optionsList);
        
        
        optionsList.Clear();
        foreach (var itemName in Enum.GetNames(typeof(CalculationType)))
        {
            optionsList.Add(new TMP_Dropdown.OptionData(itemName));
        }

        calcTypeDropDown.AddOptions(optionsList);
    }

    private bool CanCalculateRecipe()
    {
        if (selectedRecipeIndex < recipes.Count)
            return true;

        return false;
    }

    private void CalculateRecipes()
    {
        if (!CanCalculateRecipe())
            return;

        CalculationResult calcResult = calculationType == CalculationType.Resources
            ? resourcesCalculator.CalculateByOutputResourceCount(recipes[selectedRecipeIndex], itemsCount)
            : resourcesCalculator.CalculateByBlocksCount(recipes[selectedRecipeIndex], itemsCount);

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
        if (float.TryParse(inputString.Replace('.', ','), out float count))
        {
            itemsCount = count;
            CalculateRecipes();
        }
    }

    private void OnCalculationTypeChanged(int index)
    {
        calculationType = (CalculationType)index;
        CalculateRecipes();
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
