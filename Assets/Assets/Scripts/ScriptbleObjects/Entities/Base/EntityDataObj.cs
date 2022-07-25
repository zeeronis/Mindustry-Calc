using UnityEngine;


public class EntityDataObj : ScriptableObject
{
    [Header("Entity Data")]
    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;

    public int ID { get; private set; }
    public string EntityName => name;
    public Sprite Sprite => sprite;


    public EntityDataObj()
    {
       
    }

    public EntityDataObj(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }

    public void SetID(int id)
    {
        ID = id;
    }
}
