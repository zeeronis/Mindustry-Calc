using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIResourceItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Image resourceImage;

    public void Init(ResourceStack resourceStack)
    {
        countText.text = System.Math.Round(resourceStack.count, 2).ToString().Replace(',', '.');
        nameText.text = resourceStack.resourceData.EntityName;
        resourceImage.sprite = resourceStack.resourceData.Sprite;
    }
}