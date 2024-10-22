using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyItemDataRuntime
{
    [SerializeField] private string itemName;
    [SerializeField] private int count;
    public DailyItemDataRuntime(string _name, int _count)
    {
        itemName = _name;
        count = _count;
    }
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }
    public int Count
    {
        get { return count; }
        set { count = value; }
    }
}
