using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntitiesDatabaseObject", menuName = "Mindustry/EntitiesDatabaseObject", order = 0)]
public class EntitiesDatabaseObject : ScriptableObject
{
    [SerializeField] private ResourceDataObj[] resources;
    [SerializeField] private BlockDataObj[] blocks;
    [SerializeField] private BlockPumpDataObj[] pumps;

    [HideInInspector] public List<BlockDataObj> recipes;
    public ResourceDataObj[] Resources => resources;


#if UNITY_EDITOR
    private void OnDestroy()
    {
        recipes?.Clear();
    }
#endif

    public void Init()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].SetID(i);
        }

        recipes = new List<BlockDataObj>(blocks);
        foreach (BlockPumpDataObj pump in pumps)
        {
            foreach (ResourceStack liquidRes in pump.AvailableFluids)
            {
                recipes.Add(new BlockDataObj(
                    pump.Name,
                    pump.Sprite,
                    System.Array.Empty<ResourceStack>(),
                    liquidRes,
                    pump.InputEnergy,
                    1));
            }
        }

        for (int i = 0; i < recipes.Count; i++)
        {
            recipes[i].SetID(i);
        }
    }

    public List<BlockDataObj> GetRecipes(int resourceIndex)
    {
        return GetRecipes(resources[resourceIndex]);
    }

    public List<BlockDataObj> GetRecipes(ResourceDataObj resourceDataObj)
    {
        var avaliableRecipes = new List<BlockDataObj>();
        foreach (var item in recipes)
        {
            if (item.OutputResource.resourceData.ID == resourceDataObj.ID)
            {
                avaliableRecipes.Add(item);
            }
        }

        return avaliableRecipes;
    }
}