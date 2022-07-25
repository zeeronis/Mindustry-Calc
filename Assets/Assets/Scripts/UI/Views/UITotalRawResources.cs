using System.Collections;
using UnityEngine;

public class UITotalRawResources : MonoBehaviour
{
    [SerializeField] UIResourcesView resourcesView;


    public void Init(ResourceStack[] resourceStacks)
    {
        resourcesView.Init(resourceStacks);
    }
}