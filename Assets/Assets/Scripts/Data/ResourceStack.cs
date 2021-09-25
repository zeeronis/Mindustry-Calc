using System;
using UnityEngine;

[System.Serializable]
public struct ResourceStack
{
    public ResourceDataObj resourceData;
    [Min(0)] public float count;


    public ResourceStack Clone()
    {
        return new ResourceStack()
        {
            resourceData = resourceData,
            count = count
        };
    }

    public ResourceStack GetNormalizedToSecond(float produceTime)
    {
        return new ResourceStack()
        {
            resourceData = resourceData,
            count = count / produceTime
        };
    }

    public ResourceStack Get(float produceTime, float percentOfReq)
    {
        return new ResourceStack()
        {
            resourceData = resourceData,
            count = count / produceTime / percentOfReq
        };
    }
}
