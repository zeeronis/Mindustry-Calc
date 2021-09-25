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
    [SerializeField] private UIBlockInfo blockInfo;
    [SerializeField] private UICalcResult calcResultPanel;


    private int resourceCount = 0;
    private int selectedResourceIndex = 0;
    private ResourcesCalculator resourcesCalculator;


    private void Awake()
    {
        entitiesDatabase.Init();
        Init(entitiesDatabase.resources);

        resCountInputField.onValueChanged.AddListener(OnResourceValueChanged);
        resourcesDropDown.onValueChanged.AddListener(OnResourceTypeCahnged);

        resourcesCalculator = new ResourcesCalculator(entitiesDatabase);
        
        blockInfo.gameObject.SetActive(false);
        calcResultPanel.gameObject.SetActive(false);
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
        var calcResult = resourcesCalculator.Calculate(entitiesDatabase.resources[selectedResourceIndex], resourceCount);

        if (calcResult.recipeBlockData != null)
        {
            blockInfo.gameObject.SetActive(true);
            calcResultPanel.gameObject.SetActive(true);

            blockInfo.Init(calcResult.recipeBlockData);
            calcResultPanel.Init(calcResult);
        }
    }

    private void OnResourceValueChanged(string inputString)
    {
        if (int.TryParse(inputString, out int count))
        {
            resourceCount = count;
            CalculateRecipes();
        }
    }

    private void OnResourceTypeCahnged(int selectedIndex)
    {
        selectedResourceIndex = selectedIndex;
        CalculateRecipes();
    }
}
