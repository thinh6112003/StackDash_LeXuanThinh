using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePath : MonoBehaviour
{
    [SerializeField] List<Transform> listBridgePoints = new List<Transform>();
    public List<Transform> ListBridgePoints()
    {
        return listBridgePoints;
    }
}
