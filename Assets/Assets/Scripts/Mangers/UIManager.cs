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
        // temp return block data
        var blockData = resourcesCalculator.Calculate(entitiesDatabase.resources[selectedResourceIndex], resourceCount);

        blockInfo.Init(blockData); //debug
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
