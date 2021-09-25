using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntitiesDatabaseObject", menuName = "Mindustry/EntitiesDatabaseObject", order = 0)]
public class EntitiesDatabaseObject : ScriptableObject
{
    [SerializeField] ResourceDataObj[] resources;
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
        }
    }
}