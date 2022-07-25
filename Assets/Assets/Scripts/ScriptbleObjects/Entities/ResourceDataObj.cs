using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Mindustry/Resource", order = 2)]
public class ResourceDataObj : EntityDataObj
{
    [Header("Resource Data")]
    [SerializeField] bool isLiquid;
    [SerializeField] bool isBaseResource;

    public bool IsLiquid => isLiquid;
    public bool IsBaseResource => isBaseResource;
}