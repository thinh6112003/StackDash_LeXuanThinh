using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class ShopDataRuntime
{
    [SerializeField] private float cash = 150;
    [SerializeField] private int energy = 10;
    [SerializeField] private string shopName;
    [SerializeField] private int level = 1;
    [SerializeField] private int exp = 0;
    [SerializeField] private int expandedLevel = 0;
    [SerializeField] public Color shopColor;
    [SerializeField] private string launcher = "";
    [SerializeField] private bool isClose = true;
    [SerializeField] private bool hasStaff = false;

    [SerializeField] private List<ShelfDataRuntime> shelfs = new List<ShelfDataRuntime>();
    [SerializeField] public List<string> unlockedLicenses = new List<string>();
    [SerializeField] private List<BoxDataRuntime> boxes = new List<BoxDataRuntime>();
    [SerializeField] private List<CachedPrice> cachedPrices = new List<CachedPrice>();

    [SerializeField] private string timeToResetStaff = "";
    [SerializeField] private float totalDeliveryTimes = 0;
    [SerializeField] private int currentEnergyCost = 0;

    #region get & set
    public int CurrentEnergyCost
    {
        get { return currentEnergyCost; }
        set { currentEnergyCost = value; }
    }
    public DateTime TimeToResetStaff
    {
        set => timeToResetStaff = value.ToString();
        get => DateTime.Parse(timeToResetStaff);
    }
    public bool HasStaff
    {
        get { return hasStaff; }
        set { hasStaff = value; }
    }
    public bool IsClose
    {
        get { return isClose; }
        set { isClose = value; }
    }
    public int ExpandedLevel
    {
        get { return expandedLevel; }
        set { expandedLevel = value; }
    }
    public float Cash
    {
        get { return cash; }
        set { cash = value; }
    }
    public int Energy
    {
        get { return energy; }
        set { energy = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }
    public string ShopName
    {
        get { return shopName; }
        set { shopName = value; }
    }
    public string Launcher
    {
        get { return launcher; }
        set { launcher = value; }
    }
    public float TotalDeliveryTimes
    {
        get { return totalDeliveryTimes; }
        set { totalDeliveryTimes = value; }
    }
    #endregion

    public bool IsUnlockedLicense(string name)
    {
        return unlockedLicenses.Contains(name);
    }
    public bool AddLicense(string name)
    {
        if (IsUnlockedLicense(name))
            return false;
        else
        {
            unlockedLicenses.Add(name);
            return true;
        }
    }
    public void AddCash(float val)
    {
        cash = Mathf.Max(0, cash + val);
    }
    public float GetCurrentPrice(string itemName)
    {
        foreach(var shelf in shelfs)
        {
            foreach(var trigger in shelf.GetAllTriggerShelf())
            {
                //Debug.Log($" {itemName} = {trigger.ItemName } ? {trigger.CurrentPrice}");
                if(trigger.ItemName == itemName)
                {
                    return trigger.CurrentPrice;
                }
            }
        }
        return -1;
    }
    // cruzia
    public void AddExp(int _exp)
    {
        //LevelDataConfig currentLevel = DataConfigManager.Instance.GetLeveDataConfig(level);
        //if(currentLevel != null)
        //{
        //    exp += _exp;
        //    while (currentLevel != null && exp >= currentLevel.expToLevelUp)
        //    {
        //        level += 1;
        //        DataConfigManager.Instance.PlusEnergy(DataConfigManager.Instance.energyWhenLevelUp);
        //        exp -= currentLevel.expToLevelUp;
        //        currentLevel = DataConfigManager.Instance.GetLeveDataConfig(level);
        //        //Firebase.Analytics.FirebaseAnalytics.LogEvent("User_Level", new Firebase.Analytics.Parameter("Level_ID", level.ToString()));
        //    }
        //}
        //else
        //{
        //    Debug.Log("Level max...");
        //}
    }
    #region boxes
    public BoxDataRuntime AddBox(BoxDataRuntime newBox)
    {
        BoxDataRuntime box = GetBox(newBox.BoxName);
        if (box == null)
        {
            boxes.Add(newBox);
            return newBox;
        }
        else
        {
            box.ItemName = newBox.ItemName;
            box.ItemCount = newBox.ItemCount;
            box.Position = newBox.Position;
            box.EulerAngles = newBox.EulerAngles;
            return box;
        }
    }
    public void UpdateBox(BoxDataRuntime box, string ItemName, int ItemCount, 
        Vector3 Position, Vector3 EulerAngles, float deliTime/*, bool isDelivered*/)
    {
        box.ItemName = ItemName;
        box.ItemCount = ItemCount;
        box.Position = Position;
        box.EulerAngles = EulerAngles;
        box.DeliTime = deliTime;
        //box.IsDelivered = isDelivered;
    }
    public void UpdateBox(BoxDataRuntime box, int ItemCount)
    {
        box.ItemCount = ItemCount;
    }
    public void RemoveBox(BoxDataRuntime box)
    {
        if(GetBox(box.BoxName) != null)
        {
            boxes.Remove(box);
        }
    }
    public BoxDataRuntime GetBox(string boxName)
    {
        return boxes.Where(a => a.BoxName == boxName).FirstOrDefault();
    }
    public List<BoxDataRuntime> GetAllBox()
    {
        return boxes;
    }
    #endregion

    #region shelf & decor
    public ShelfDataRuntime AddShelf(ShelfDataRuntime newShelf)
    {
        ShelfDataRuntime shelf = GetShelf(newShelf.NameID);
        if(shelf == null)
        {
            shelfs.Add(newShelf);
            return newShelf;
        }
        else
        {
            shelf.ShelfID = newShelf.ShelfID;
            shelf.Position = newShelf.Position;
            shelf.EulerAngles = newShelf.EulerAngles;
            return shelf;
        }
    }
    public void UpdateShelf(ShelfDataRuntime shelf, int _shelfID, Vector3 _position, Vector3 _eulerAngles)
    {
        shelf.ShelfID = _shelfID;
        shelf.Position = _position;
        shelf.EulerAngles = _eulerAngles;
    }
    public ShelfDataRuntime GetShelf(string shelfName)
    {
        return shelfs.Where(a => a.NameID == shelfName).FirstOrDefault();
    }
    public List<ShelfDataRuntime> GetAllShelf()
    {
        return shelfs;
    }
    #endregion

    private CachedPrice GetCachedPrice(string product)
    {
        return cachedPrices.Where(a => a.product.Equals(product)).FirstOrDefault();
    }
    public float GetCachedPriceOf(string product)
    {
        CachedPrice cachedPrice = GetCachedPrice(product);
        if (cachedPrice != null)
            return cachedPrice.price;

        return -1;
    }
    public void SetCachedPriceOf(string product, float price)
    {
        CachedPrice cachedPrice = GetCachedPrice(product);
        if (cachedPrice != null)
            cachedPrice.price = price;
        else
        {
            cachedPrices.Add(new CachedPrice(product, price));
        }
    }

    [System.Serializable]
    public struct Color
    {
        public float r;
        public float g;
        public float b;
        public float a;
    }
    
    [System.Serializable]
    public class CachedPrice
    {
        public string product;
        public float price;

        public CachedPrice(string _product, float _price)
        {
            product = _product;
            price = _price;
        }
    }
}
