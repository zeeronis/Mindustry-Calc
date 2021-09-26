using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnItemChanged(int index);

public class UIRecipesView : MonoBehaviour
{
    [SerializeField] private UIBlockPreviewInfo blockPreviewPrefab;
    [Space]
    [SerializeField] private RectTransform contentTransform;

    private readonly List<UIBlockPreviewInfo> blockItems = new List<UIBlockPreviewInfo>();

    public event OnItemChanged OnItemChanged;


    private void DestroyItems()
    {
        foreach (var item in blockItems)
        {
            item.OnClick -= OnItemClick;
            Destroy(item.gameObject);
        }

        blockItems.Clear();
    } 

    private void UpdateSelectionColors(int selectedItemIndex)
    {
        for (int i = 0; i < blockItems.Count; i++)
        {
            blockItems[i].SetIsSelected(i == selectedItemIndex);
        }
    }

    private void OnItemClick(int index)
    {
        UpdateSelectionColors(index);
        OnItemChanged?.Invoke(index);
    }

    public void Init(List<BlockDataObj> blockDatas)
    {
        DestroyItems();

        for (int i = 0; i < blockDatas.Count; i++)
        {
            var uiBlockPreview = Instantiate(blockPreviewPrefab, contentTransform);

            uiBlockPreview.Init(blockDatas[i], i);
            uiBlockPreview.OnClick += OnItemClick;

            blockItems.Add(uiBlockPreview);
        }

        UpdateSelectionColors(0);
    }
}