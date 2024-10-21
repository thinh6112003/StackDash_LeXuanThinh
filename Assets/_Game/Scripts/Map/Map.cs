using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private LastPiece planeEndGame;
    public void getDataMap(ref LastPiece lastPieceOfPlayer,ref Vector3 starPosOfPlayer)
    {
        lastPieceOfPlayer = planeEndGame;
        starPosOfPlayer = startPos.position;
    }
}
