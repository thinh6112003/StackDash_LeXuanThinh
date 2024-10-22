using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShelfDataRuntime
{
    [SerializeField] private string nameId;
    [SerializeField] private int shelfId;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 eulerAngles;

    [SerializeField] private List<TriggerShelfDataRuntime> triggerShelfs = new List<TriggerShelfDataRuntime>();

    public string NameID
    {
        get { return nameId; }
        set { nameId = value; }
    }
    public int ShelfID
    {
        get { return shelfId; }
        set { shelfId = value; }
    }
    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    public Vector3 EulerAngles
    {
        get { return eulerAngles; }
        set { eulerAngles = value; }
    }
    public ShelfDataRuntime(string _nameId, int _shelfId, Vector3 _position, Vector3 _eulerAngles)
    {
        nameId = _nameId;
        shelfId = _shelfId;
        position = _position;
        eulerAngles = _eulerAngles;
    }
    public List<TriggerShelfDataRuntime> GetAllTriggerShelf()
    {
        return triggerShelfs;
    } 
    public void GenerateTriggerShelf(int count)
    {
        triggerShelfs.Clear();
        for (int i = 0; i < count; i++)
        {
            triggerShelfs.Add(new TriggerShelfDataRuntime());
        }
    }
}
