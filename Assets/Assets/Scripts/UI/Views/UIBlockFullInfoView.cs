using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIBlockFullInfoView : MonoBehaviour
{
    [SerializeField] private UIResourceItem resourceItemPrefab;
    [Space]
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI blockNameText;
    [SerializeField] private GameObject powerPlaneGameObject;
    [SerializeField] private TextMeshProUGUI powerUseText;
    [SerializeField] private TextMeshProUGUI productionTimeText;
    [Space]
    [SerializeField] private UIResourcesView inputResourcesView;
    [SerializeField] private UIResourceItem outputResourceItem;


    public void Init(BlockDataObj blockDataObj)
    {
        blockImage.sprite = blockDataObj.Sprite;
        blockNameText.text = blockDataObj.Name;
        productionTimeText.text = $"Production Time: {blockDataObj.ProduceTime} sec";

        if (blockDataObj.InputEnergy == 0)
        {
            powerPlaneGameObject.SetActive(false);
        }
        else
        {
            powerPlaneGameObject.SetActive(true);
            powerUseText.text = $"Power Use {blockDataObj.InputEnergy}/sec";
        }

        inputResourcesView.Init(blockDataObj.InputResources);
        outputResourceItem.Init(blockDataObj.OutputResource);
    }
}