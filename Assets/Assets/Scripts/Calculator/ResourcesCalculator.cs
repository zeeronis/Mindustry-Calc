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

    public CalculationResult CalculateByOutputResourceCount(BlockDataObj recipe, float reqOutputCount) 
    {
        CalculationResult calculationResult = default;

        ResourceStack outputResource = recipe.OutputResource.GetNormalizedToSecond(recipe.ProduceTime);
        float percentOfReq = outputResource.count / reqOutputCount;
        int blocksUseCount = Mathf.CeilToInt(reqOutputCount / outputResource.count);

        ResourceStack[] inputResources = new ResourceStack[recipe.InputResources.Length];
        for (int i = 0; i < inputResources.Length; i++)
        {
            inputResources[i] = recipe.InputResources[i].GetNormalizedByOutput(recipe.ProduceTime, percentOfReq);
        }

        outputResource.count = reqOutputCount;

        calculationResult.powerUse = recipe.InputEnergy * blocksUseCount;
        calculationResult.inputResources = inputResources;
        calculationResult.outputResource = outputResource;
        calculationResult.blocksUseCount = blocksUseCount;
        calculationResult.recipeBlockData = recipe;

        return calculationResult;
    }

    public CalculationResult CalculateByBlocksCount(BlockDataObj recipe, float blocksCount)
    {
        CalculationResult calculationResult = default;

        ResourceStack outputResource = recipe.OutputResource.GetNormalizedToSencondAndBlocksCount(recipe.ProduceTime, blocksCount);
        ResourceStack[] inputResources = new ResourceStack[recipe.InputResources.Length];
        for (int i = 0; i < inputResources.Length; i++)
        {
            inputResources[i] = recipe.InputResources[i].GetNormalizedToSencondAndBlocksCount(recipe.ProduceTime, blocksCount);
        }

        calculationResult.powerUse = recipe.InputEnergy * blocksCount;
        calculationResult.inputResources = inputResources;
        calculationResult.outputResource = outputResource;
        calculationResult.blocksUseCount = blocksCount;
        calculationResult.recipeBlockData = recipe;

        return calculationResult;
    }
}
