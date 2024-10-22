using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCDataRuntime
{
    [SerializeField] public List<NPCData> nPCDatas = new List<NPCData>();

    public NPCData GetNpcData(string uniqueID)
    {
        for(int i = 0; i < nPCDatas.Count; i++)
        {
            if (nPCDatas[i].uniqueID == uniqueID) return nPCDatas[i];
        }

        return null;
    }
    public void AddNPCData(NPCData npcData)
    {
        nPCDatas.Add(npcData);
    }
    public void RemoveNPCData(string uniqueID)
    {
        for(int i = 0; i < nPCDatas.Count; i++)
        {
            if (nPCDatas[i].uniqueID == uniqueID) nPCDatas.RemoveAt(i);
        }
    }
    public bool IsContain(string uniqueID)
    {
        for(int i = 0; i < nPCDatas.Count; i++)
        {
            if (uniqueID == nPCDatas[i].uniqueID) return true;
        }

        return false;
    }
}

[System.Serializable]
public class NPCData
{
    [SerializeField] public string uniqueID;
    [SerializeField] public int skinID;
    [SerializeField] public float agentSpeed;
    
    [Header("Transform")]
    [SerializeField] public Vector3 Target;

    [SerializeField] public Vector3 StartPosition;

    [SerializeField] public Vector3 EndPosition;

    [SerializeField] public Vector3 LookAtWindow;

    [SerializeField] public Vector3 PayingZone;

    [SerializeField] public Vector3 PayingZoneAngle;

    [SerializeField] public Vector3 curTrans;

    [SerializeField] public Vector3 curRotate;

    [Header("Boolean Manager")]
    [SerializeField] public bool IsGoAway;

    [SerializeField] public bool Arrived;

    [SerializeField] public bool IsHoldWaiting;

    [SerializeField] public bool LastStep;

    [SerializeField] public bool FixedEmpty;

    [SerializeField] public bool IsArrivedCashier;

    [SerializeField] public bool FinishShopping;

    [SerializeField] public bool ItsTooLong;

    [SerializeField] public bool CheckpriceOnce;

    [SerializeField] public bool ShouldGo;

    [SerializeField] public bool GoInShop;

    [SerializeField] public bool CanGoInShop;

    [Header("Transform")]
    [SerializeField] public bool ShoppingBasket;

    [SerializeField] public bool Paperbag;

    [SerializeField] public bool Bankcard;

    [SerializeField] public bool CashHandy;

    [Header("Floating")]
    [SerializeField] public float TimeFixed;

    [SerializeField] public float WaitingTime;

    [SerializeField] public float IsWaitingTooLong;

    [SerializeField] public float PriceTotal;

    [SerializeField] public float TimeInStore;

    [SerializeField] public int minItemWillBuy;

    [Header("Modes")]
    //[SerializeField] public ModePayement Mode;
    //[SerializeField] public NPCState npcState;
    [SerializeField] public string goToEndpointNotify = null;
    
    [Header("Information")]
    [SerializeField] public List<ItemInforStruct> Items = new();
}
[System.Serializable]
public struct ItemInforStruct
{
    public float priceItem;
    public string itemName;
}
