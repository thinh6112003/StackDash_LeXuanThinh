using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoxDataRuntime
{
    [SerializeField] private string boxName;
    [SerializeField] private string itemName;
    [SerializeField] private int itemCount;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 eulerAngles;
    [SerializeField] private float deliTime;
    [SerializeField] private int energyCost;
    [SerializeField] private bool isDelivered;
    public BoxDataRuntime(string _boxName, string _itemName, int _itemCount,
        Vector3 _position, Vector3 _eulerAngles, float _deliTime, int _energyCost/*, bool _isDelivered*/)
    {
        this.boxName = _boxName;
        this.itemName = _itemName;
        this.itemCount = _itemCount;
        this.position = _position;
        this.eulerAngles = _eulerAngles;
        this.deliTime = _deliTime;
        this.energyCost = _energyCost;
        //this.isDelivered = _isDelivered;
    }
    public bool IsDelivered
    {
        get => isDelivered;
        set { isDelivered = value; }

    }
    public string BoxName
    {
        get => boxName;
        set { boxName = value; }
    }
    public string ItemName
    {
        get => itemName;
        set { itemName = value; }
    }
    public int ItemCount
    {
        get => itemCount;
        set { itemCount = value; }
    }
    public Vector3 Position
    {
        get => position;
        set { position = value; }
    }
    public Vector3 EulerAngles
    {
        get => eulerAngles;
        set { eulerAngles = value; }
    }
    public float DeliTime
    {
        get => deliTime;
        set { deliTime = value; }
    }
    public int EnergyCost
    {
        get => energyCost;
        set { energyCost = value; }
    }
    public bool IsSameBox(BoxDataRuntime box)
    {
        return boxName.Equals(box.boxName);
    }
}
