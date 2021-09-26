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


    public BlockDataObj(string name, Sprite sprite, ResourceStack[] inputResources, ResourceStack outputResource, int inputEnergy, float produceTime) : base(name, sprite)
    {
        this.inputResources = inputResources;
        this.outputResource = outputResource;
        this.inputEnergy = inputEnergy;
        this.produceTime = produceTime;
    }
}