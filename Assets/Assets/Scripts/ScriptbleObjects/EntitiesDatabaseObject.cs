using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntitiesDatabaseObject", menuName = "Mindustry/EntitiesDatabaseObject", order = 0)]
public class EntitiesDatabaseObject : ScriptableObject
{
    public ResourceDataObj[] resources;
    public BlockDataObj[] blocks;


    public void Init()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].SetID(i);
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].SetID(i);
           // blocks[i].SetID1();
        }
    }

    public List<BlockDataObj> GetRecipes(int resourceIndex)
    {
        return GetRecipes(resources[resourceIndex]);
    }

    public List<BlockDataObj> GetRecipes(ResourceDataObj resourceDataObj)
    {
        var recipes = new List<BlockDataObj>();
        foreach (var item in blocks)
        {
            if (item.OutputResource.resourceData.ID == resourceDataObj.ID)
            {
                recipes.Add(item);
            }
        }

        return recipes;
    }
}