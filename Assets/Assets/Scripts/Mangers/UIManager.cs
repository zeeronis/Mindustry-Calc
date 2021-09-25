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


    private void Awake()
    {
        
    }

    public void Init(List<ResourceDataObj> resourcesList)
    {
        resourcesDropDown.ClearOptions();

        var optionsList = new List<TMP_Dropdown.OptionData>();
        foreach (ResourceDataObj resDataObj in resourcesList)
        {
            optionsList.Add(new TMP_Dropdown.OptionData(resDataObj.Name, resDataObj.Sprite));
        }

        resourcesDropDown.AddOptions(optionsList);
    }
}
