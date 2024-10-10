using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private BridgePath myBridgePath;
    [SerializeField] private EntranceBridge myEntranceBridge;
    [SerializeField] private EndBridge myEndBridge;
    //[SerializeField] private EndBridge 
    public BridgePath MyBridgePath()
    {
        return myBridgePath;
        
    }
    public EndBridge MyEndBridge()
    {
        return myEndBridge;
    }
    public EntranceBridge MyEntranceBridge()
    {
        return myEntranceBridge;
    }

    public bool checkBrick(int index)
    {
        return myBridgePath.checkBrick(index);
    }
    public void setBrickStatus(int index, bool status)
    {
        myBridgePath.setBrickStatus(index, status);
    }
}
