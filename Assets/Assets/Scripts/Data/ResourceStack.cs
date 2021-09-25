using UnityEngine;

[System.Serializable]
public struct ResourceStack
{
    public ResourceDataObj resourceData;
    [Min(0)] public int count;
}
