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
            count = resourceData.IsLiquid
            ? count
            : count / produceTime
        };
    }

    public ResourceStack GetNormalizedByOutput(float produceTime, float percentOfReq)
    {
        return new ResourceStack()
        {
            resourceData = resourceData,
            count = resourceData.IsLiquid
                ? count / percentOfReq
                : count / produceTime / percentOfReq
        };
    }

    public ResourceStack GetNormalizedToSencondAndBlocksCount(float produceTime, float blocksCount)
    {
        return new ResourceStack()
        {
            resourceData = resourceData,
            count = resourceData.IsLiquid
                ? count * blocksCount
                : count / produceTime * blocksCount
        };
    }
}
