using UnityEngine;

[CreateAssetMenu(fileName = "BlockPump", menuName = "Mindustry/BlockPump", order = 1)]
public class BlockPumpDataObj : EntityDataObj
{
    [Header("Pump Data")]
    [SerializeField] private ResourceStack[] availableFluids;
    [Space]
    [SerializeField] private int inputEnergy;

    public int InputEnergy => inputEnergy;
    public ResourceStack[] AvailableFluids => availableFluids;
}