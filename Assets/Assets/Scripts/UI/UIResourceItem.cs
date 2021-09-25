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
        countText.text = resourceStack.count.ToString();
        nameText.text = resourceStack.resourceData.Name;
        resourceImage.sprite = resourceStack.resourceData.Sprite;
    }
}