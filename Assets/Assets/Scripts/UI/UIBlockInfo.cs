using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIBlockInfo : MonoBehaviour
{
    [SerializeField] private UIResourceItem resourceItemPrefab;
    [Space]
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI blockNameText;
    [SerializeField] private GameObject powerPlaneGameObject;
    [SerializeField] private TextMeshProUGUI powerUseText;
    [SerializeField] private TextMeshProUGUI productionTimeText;
    [Space]
    [SerializeField] private RectTransform inputResourcesTransform;
    [SerializeField] private UIResourceItem outputResourceItem;

    private bool cache;
    private float startInputResourcesHeight;
    private float resourceItemHeight;
    private List<UIResourceItem> resourceItemsList = new List<UIResourceItem>();


    private void DestroyInputItems()
    {
        foreach (var item in resourceItemsList)
        {
            Destroy(item.gameObject);
        }

        resourceItemsList.Clear();
    }

    public void Init(BlockDataObj blockDataObj)
    {
        if (!cache)
        {
            cache = true;
            startInputResourcesHeight = inputResourcesTransform.rect.height;
            resourceItemHeight = resourceItemPrefab.GetComponent<RectTransform>().rect.height;
        }

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

        DestroyInputItems();
        outputResourceItem.Init(blockDataObj.OutputResource);
        foreach (ResourceStack resourceStack in blockDataObj.InputResources)
        {
            var uiResourceItem = Instantiate(resourceItemPrefab, inputResourcesTransform);

            uiResourceItem.Init(resourceStack);
            resourceItemsList.Add(uiResourceItem);
        }

        inputResourcesTransform.sizeDelta = new Vector2(
            inputResourcesTransform.sizeDelta.x,
            startInputResourcesHeight + resourceItemHeight * resourceItemsList.Count);
    }
}