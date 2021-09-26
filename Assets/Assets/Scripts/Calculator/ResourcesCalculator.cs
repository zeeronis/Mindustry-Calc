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

    public CalculationResult Calculate(BlockDataObj recipe, float reqCount) 
    {
        CalculationResult calculationResult = default;

        ResourceStack outputResource = recipe.OutputResource.GetNormalizedToSecond(recipe.ProduceTime);
        float percentOfReq = outputResource.count / reqCount;
        int blocksUseCount = Mathf.CeilToInt(reqCount / outputResource.count);

        ResourceStack[] inputResources = new ResourceStack[recipe.InputResources.Length];
        for (int i = 0; i < inputResources.Length; i++)
        {
            inputResources[i] = recipe.InputResources[i].GetNormalizedByOutput(recipe.ProduceTime, percentOfReq);
        }


        calculationResult.powerUse = recipe.InputEnergy * blocksUseCount;
        calculationResult.inputResources = inputResources;
        calculationResult.blocksUseCount = blocksUseCount;
        calculationResult.recipeBlockData = recipe;

        return calculationResult;
    }
}
