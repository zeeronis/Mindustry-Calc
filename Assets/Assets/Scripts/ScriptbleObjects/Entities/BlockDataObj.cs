using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Mindustry/Block", order = 3)]
public class BlockDataObj : EntityDataObj
{
    [Header("Block Data")]
    [SerializeField] private ResourceStack[] inputResources;
    [SerializeField] private ResourceStack outputResource;
    [Space]
    [SerializeField] private int inputEnergy;
    [SerializeField] private float produceTime;

    public int InputEnergy => inputEnergy;
    public ResourceStack[] InputResources => inputResources;
    public ResourceStack OutputResource => outputResource;
    public float ProduceTime => produceTime;
}