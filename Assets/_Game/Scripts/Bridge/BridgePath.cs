using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePath : MonoBehaviour
{
    [SerializeField] List<Transform> listBridgePoints = new List<Transform>();
    [SerializeField] List<bool> listBrickPointsCheck =new List<bool>();
    private void Start()
    {
        for(int i= 0;i< listBridgePoints.Count; i++)
        {
            listBrickPointsCheck.Add(false);
        }
    }
    public bool checkBrick(int index)
    {
        if (index == listBrickPointsCheck.Count) return true;
        if (listBrickPointsCheck[index] == true) return true;
        return false;
    }

    public void setBrickStatus(int index, bool status)
    {
        listBrickPointsCheck[index] = status;
    }
    public List<Transform> ListBridgePoints()
    {
        return listBridgePoints;
    }
}
