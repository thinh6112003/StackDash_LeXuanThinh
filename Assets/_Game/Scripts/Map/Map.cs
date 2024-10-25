using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private LastPiece planeEndGame;
    [SerializeField] private Color colorDust1;
    [SerializeField] private Color colorDust2;
    public void getDataMap(ref LastPiece lastPieceOfPlayer,ref Vector3 starPosOfPlayer)
    {
        lastPieceOfPlayer = planeEndGame;
        starPosOfPlayer = startPos.position;
    }
    public (Color, Color) GetColorDust()
    {
        return (colorDust1, colorDust2);
    }
}
