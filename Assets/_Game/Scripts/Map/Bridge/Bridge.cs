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
    public void TurnOnCheckInOut()
    {
        myEndBridge.gameObject.GetComponent<Collider>().enabled = (true);
        myEntranceBridge.gameObject.GetComponent<Collider>().enabled = (true);
    }
    public void TurnOffCheckInOut()
    {
        myEndBridge.gameObject.GetComponent<Collider>().enabled=(false);
        myEntranceBridge.gameObject.GetComponent<Collider>().enabled = (false);
    }
    public EntranceBridge MyEntranceBridge()
    {
        return myEntranceBridge;
    }

    public bool checkBrickStatus(int index)
    {
        return myBridgePath.checkBrickStatus(index);
    }
    public void setBrickStatus(int index, bool status)
    {
        myBridgePath.setBrickStatus(index, status);
    }
}
