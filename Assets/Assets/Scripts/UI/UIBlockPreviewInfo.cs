using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIBlockPreviewInfo: MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI blockNameText;
    [SerializeField] private GameObject highlightImage;

    private int itemIndex;
    public event OnItemChanged OnClick;


    public void Init(BlockDataObj blockData, int itemIndex)
    {
        this.itemIndex = itemIndex;

        blockImage.sprite = blockData.Sprite;
        blockNameText.text = blockData.Name;
    }

    public void SetIsSelected(bool isSelected)
    {
        highlightImage.SetActive(isSelected);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(itemIndex);
    }
}
