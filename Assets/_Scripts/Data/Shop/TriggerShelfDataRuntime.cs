using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriggerShelfDataRuntime
{
    [SerializeField] private string itemName;
    [SerializeField] private float currentPrice;
    [SerializeField] private string typeShelf;
    [SerializeField] private bool[] slots;
    public string ItemName
    {
        get => itemName;
        set { itemName = value; }
    }
    public string TypeShelf
    {
        get => typeShelf;
        set { typeShelf = value; }
    }
    public float CurrentPrice
    {
        get => currentPrice;
        set { currentPrice = value; }
    }
    public bool[] Slots
    {
        get => slots;
        set { slots = value; }
    }
}
