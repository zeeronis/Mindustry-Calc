using UnityEngine;


public class EntityDataObj : ScriptableObject
{
    [Header("Entity Data")]
    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;

    public int ID { get; private set; }
    public string Name => name;
    public Sprite Sprite => sprite;


    public void SetID(int id)
    {
        ID = id;
    }

    public void SetID1()
    {
        name = "123";
    }
}
