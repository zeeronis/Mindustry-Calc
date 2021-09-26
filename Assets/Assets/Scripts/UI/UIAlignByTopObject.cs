using System.Collections;
using UnityEngine;

public class UIAlignByTopObject : MonoBehaviour
{
    [SerializeField] private RectTransform myTransform;
    [SerializeField] private RectTransform topObjectTransrom;
    [SerializeField] private float offsetY;


    private void Update()
    {
        AlignPosition(); // TODO optimize UIAlignByTopObject
    }

    public void AlignPosition()
    {
        Vector3 position = myTransform.anchoredPosition;
        position.y = topObjectTransrom.anchoredPosition.y - topObjectTransrom.sizeDelta.y + offsetY;
        myTransform.anchoredPosition = position;
    }
}