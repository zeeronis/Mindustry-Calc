using System.Collections;
using UnityEngine;
using TMPro;

public class UICalcResult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI powerUseText;
    [SerializeField] TextMeshProUGUI blocksUseText;
    [SerializeField] UIResourcesView resourcesView;


    public void Init(in CalculationResult calculationResult)
    {
        blocksUseText.text = $"Blocks Use: {calculationResult.blocksUseCount}";
        powerUseText.text = $"Power Use: {calculationResult.powerUse}/sec";
        resourcesView.Init(calculationResult.inputResources);
    }
}