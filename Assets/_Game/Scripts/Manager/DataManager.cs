using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] bool isdebuglevel;
    public DynamicData dynamicData;
    public int score = 0;
    public int earned = 100;
    private const string keyGetSetData = "DynamicData";
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        dynamicData = DataRuntimeManager.Instance.dynamicData;
    }
    
    private void Start()
    {
        Observer.AddListener(conststring.DONELOADLEVEL, InitGame);
        Observer.AddListener(conststring.LAYBRICK, UpdateScore);
        Observer.AddListener(conststring.FINISHGAME, FinishGame);
        Observer.AddListener(conststring.NEXTLEVEL, () =>
        {
            dynamicData.AddCoin(earned);
        });
    }

    private void FinishGame()
    {
        dynamicData.NextCurrentIDLevel();
    }

    private void UpdateScore()
    {
        score++;
        Observer.Noti(conststring.UPDATEUI);
    }
    public int GetScore()
    {
        return score;
    }
    public void IncScore()
    {

        score++;
        Observer.Noti(conststring.UPDATEUI);
    }

    private void InitGame()
    {
        score = 0;
        Observer.Noti(conststring.UPDATEUI);
    }
    public int GetEarned()
    {
        return earned;
    }
}
