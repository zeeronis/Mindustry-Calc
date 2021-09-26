using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class UIResourcesView : MonoBehaviour
{
    [SerializeField] private UIResourceItem resourceItemPrefab;
    [SerializeField] private RectTransform contentTransform;

    private bool cache;
    private float resourceItemHeight;
    private float startInputResourcesHeight;
    private List<UIResourceItem> resourceItemsList = new List<UIResourceItem>();


    private void DestroyItems()
    {
        foreach (var item in resourceItemsList)
        {
            Destroy(item.gameObject);
        }

        resourceItemsList.Clear();
    }

    private void FitSize()
    {
        contentTransform.sizeDelta = new Vector2(
            contentTransform.sizeDelta.x,
            startInputResourcesHeight + resourceItemHeight * resourceItemsList.Count);
    }

    public void Init(ResourceStack[] resourceStacks)
    {
        if (!cache)
        {
            cache = true;
            startInputResourcesHeight = contentTransform.rect.height;
            resourceItemHeight = resourceItemPrefab.GetComponent<RectTransform>().rect.height;
        }

        DestroyItems();
        foreach (ResourceStack resourceStack in resourceStacks)
        {
            var uiResourceItem = Instantiate(resourceItemPrefab, contentTransform);

            uiResourceItem.Init(resourceStack);
            resourceItemsList.Add(uiResourceItem);
        }

        FitSize();
        gameObject.SetActive(resourceStacks.Length != 0);
    }
}