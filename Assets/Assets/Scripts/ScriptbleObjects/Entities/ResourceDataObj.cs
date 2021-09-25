using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Mindustry/Resource", order = 2)]
public class ResourceDataObj : EntityDataObj
{
    [Header("Resource Data")]
    [SerializeField] bool isLiquid;

    public bool IsLiquid => isLiquid;
}