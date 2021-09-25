﻿using System;
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

    public CalculationResult Calculate(ResourceDataObj resourceData, float reqCount)
    {
        CalculationResult calculationResult = default;

        var recipes = databaseObject.GetRecipes(resourceData);
        if (recipes.Count == 0)
        {
            Debug.LogWarning($"No recipes found for {resourceData.Name} resource");
            return calculationResult;
        }

        var recipe = recipes[0];

        ResourceStack outputResource = recipe.OutputResource.GetNormalizedToSecond(recipe.ProduceTime);
        float percentOfReq = outputResource.count / reqCount;

        ResourceStack[] inputResources = new ResourceStack[recipe.InputResources.Length];
        for (int i = 0; i < inputResources.Length; i++)
        {
            inputResources[i] = recipe.InputResources[i].Get(recipe.ProduceTime, percentOfReq);
        }

        int blocksUseCount = Mathf.CeilToInt(reqCount / outputResource.count);
        calculationResult.powerUse = recipe.InputEnergy * blocksUseCount;
        calculationResult.inputResources = inputResources;
        calculationResult.blocksUseCount = blocksUseCount;
        calculationResult.recipeBlockData = recipe;

        return calculationResult;
    }
}
