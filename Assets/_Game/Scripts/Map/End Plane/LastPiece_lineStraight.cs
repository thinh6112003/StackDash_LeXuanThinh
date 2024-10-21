using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPiece_lineStraight : MonoBehaviour
{
    [SerializeField] private List<Transform> listLayBrick = new List<Transform>();
    private int idCurrent = 0 ;
    public Transform GetNextLayBrickPoint()
    {
        if (idCurrent == listLayBrick.Count) return null;
        return listLayBrick[idCurrent++];
    }
}
