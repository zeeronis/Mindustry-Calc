using System.Collections;
using UnityEngine;
using TMPro;

public class UICalcResultView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI powerUseText;
    [SerializeField] TextMeshProUGUI blocksUseText;
    [Space]
    [SerializeField] UIResourceItem outputResourceItem;
    [SerializeField] UIResourcesView inputResourcesView;
    

    public void Init(in CalculationResult calculationResult)
    {
        blocksUseText.text = $"Blocks Use: {calculationResult.blocksUseCount}";
        powerUseText.text = $"Power Use: {calculationResult.powerUse}/sec";

        inputResourcesView.Init(calculationResult.inputResources);
        outputResourceItem.Init(calculationResult.outputResource);
    }
}