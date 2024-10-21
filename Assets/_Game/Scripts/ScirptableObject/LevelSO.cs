using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "ScriptableObjects/LevelSO")]
public class LevelSO : ScriptableObject 
{
    public List<LevelData> levels= new List<LevelData>();
    public LevelData GetLevelByID(int iD)
    {
        return levels[iD];
    }
    public Map GetMapByLevelID(int iD)
    {
        return levels[iD-1].map;
    }
}
