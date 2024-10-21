using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPiece : MonoBehaviour
{
    [SerializeField] List<LastPiece_lineStraight> listLastPieceLS = new List<LastPiece_lineStraight>();
    private int iDLastPieceLSCurrent = 0;
    public Transform GetNextLayBrickPoint()
    {
        Transform layBrickTf = listLastPieceLS[iDLastPieceLSCurrent].GetNextLayBrickPoint();
        if (layBrickTf == null)
        {
            iDLastPieceLSCurrent++;
            if (iDLastPieceLSCurrent == listLastPieceLS.Count) return null;
            return listLastPieceLS[iDLastPieceLSCurrent].GetNextLayBrickPoint();
        }
        return layBrickTf;
    }
}
