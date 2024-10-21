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
        LoadData();
        if(isdebuglevel)
        {
            Debug.Log("dang trong che do debug level1");
            dynamicData = new DynamicData();
            //dynamicData.SetCurrentIDLevel (2);
        }
        else
        {
            if (dynamicData == null) {
                dynamicData = new DynamicData();
                SaveData();
            }
        }
    }
    private void Start()
    {
        Observer.AddListener(conststring.INITGAME, InitGame);
        Observer.AddListener(conststring.LAYBRICK, UpdateScore);
        Observer.AddListener(conststring.NEXTLEVEL, () =>
        {
            dynamicData.AddCoin(earned);
        });
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
        Debug.Log("incscore");
    }

    private void InitGame()
    {
        score = 0;
    }

    public void LoadData()
    {
        string dynamicDataString = PlayerPrefs.GetString(keyGetSetData);
        dynamicData = JsonUtility.FromJson<DynamicData>(dynamicDataString);
    }
    public void SaveData()
    {
        string dynamicDataString = JsonUtility.ToJson(dynamicData);
        PlayerPrefs.SetString(keyGetSetData, dynamicDataString);
    }
    private void OnApplicationFocus(bool focus)
    {
        SaveData();
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }

    public int GetEarned()
    {
        return earned;
    }
}
