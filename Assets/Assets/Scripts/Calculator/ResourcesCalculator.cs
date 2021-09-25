using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ResourcesCalculator
{
    private EntitiesDatabaseObject databaseObject;

    public ResourcesCalculator(EntitiesDatabaseObject databaseObject)
    {
        this.databaseObject = databaseObject;
    }

    public BlockDataObj Calculate(ResourceDataObj resourceData, float reqCount)
    {
        var recipes = databaseObject.GetRecipes(resourceData);
        var recipe = recipes[0];

        ResourceStack outputResource = recipe.OutputResource.GetNormalizedToSecond(recipe.ProduceTime);
        float percentOfReq = outputResource.count / reqCount;

        ResourceStack[] inputResources = new ResourceStack[recipe.InputResources.Length];
        for (int i = 0; i < inputResources.Length; i++)
        {
            inputResources[i] = recipe.InputResources[i].Get(recipe.ProduceTime, percentOfReq);
        }

        int reqBlocksCout = Mathf.CeilToInt(reqCount / outputResource.count);

        Debug.Log($"***");
        Debug.Log($"reqResCount: {reqCount}");
        Debug.Log($"reqBlocksCount: {reqBlocksCout}");
        Debug.Log($"reqPower: {recipe.InputEnergy * reqBlocksCout}");
        foreach (var item in inputResources)
        {
            Debug.Log($"inputRes: {item.resourceData.Name} {item.count}");
         //   Debug.Log($"inputPower: {recipe.=}");
        }

        return recipe;
    }
}
