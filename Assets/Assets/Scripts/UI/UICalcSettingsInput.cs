using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICalcSettingsInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField resCountInputField;
    [SerializeField] private TMP_Dropdown resourcesDropDown;
    [SerializeField] private TMP_Dropdown calcTypeDropDown;


    public float ItemsCount { get; private set; }
    public string SelectedResourceName { get; private set; }
    public CalculationType CalcType { get; private set; }


    public event System.Action OnSettingsChanged;


    private void Awake()
    {
        resCountInputField.onValueChanged.AddListener(OnResourceValueChanged);
        resourcesDropDown.onValueChanged.AddListener(OnResourceTypeChanged);
        calcTypeDropDown.onValueChanged.AddListener(OnCalculationTypeChanged);
    }

    private float ParseString_WebGL(string input)
    {
        input = input.Replace('.', ',');
        
        int pos = input.IndexOf(",");
        if (pos < 0)
            return float.Parse(input);

        int power = input.Length - pos - 1;

        float value = float.Parse(input.Remove(pos, 1));
        float divideBy = Mathf.Pow(10, power);

        return value / divideBy;
    }


    private void OnResourceValueChanged(string inputString)
    {
        if (inputString == "-")
            return;

        ItemsCount = ParseString_WebGL(inputString);
        OnSettingsChanged?.Invoke();
    }

    private void OnResourceTypeChanged(int selectedIndex)
    {
        SelectedResourceName = resourcesDropDown.options[selectedIndex].text;
        OnSettingsChanged?.Invoke();
    }

    private void OnCalculationTypeChanged(int index)
    {
        CalcType = (CalculationType)index;
        OnSettingsChanged?.Invoke();
    }


    public void Init(ResourceDataObj[] resourcesList)
    {
        resourcesDropDown.ClearOptions();
        calcTypeDropDown.ClearOptions();


        var optionsList = new List<TMP_Dropdown.OptionData>();
        foreach (ResourceDataObj resDataObj in resourcesList)
        {
            if (resDataObj.IsBaseResource && !resDataObj.IsLiquid)
                continue;

            optionsList.Add(new TMP_Dropdown.OptionData(resDataObj.Name, resDataObj.Sprite));
        }

        resourcesDropDown.AddOptions(optionsList);


        optionsList.Clear();
        foreach (var itemName in System.Enum.GetNames(typeof(CalculationType)))
        {
            optionsList.Add(new TMP_Dropdown.OptionData(itemName));
        }

        calcTypeDropDown.AddOptions(optionsList);


        OnResourceTypeChanged(0);
    }
}