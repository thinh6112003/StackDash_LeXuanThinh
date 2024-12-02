using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class DynamicData
{
    public int currentIDLevel;
    public int currentCoin;
    public int currentGem;
    public string currentSkinName;
    public bool soundStatus;
    public bool vibrationStatus;

    public DynamicData()
    {
        currentIDLevel = 1;
        currentCoin = 100;
        currentGem = 100;
        currentSkinName = "";
        soundStatus = true;
        vibrationStatus = true;
    }
    public void SetActiveSoundStatus(bool status)
    {
        soundStatus = status;
        Observer.Noti(conststring.UPDATESOUNDVIBRATION);
    }
    public void SetVibrationStatus(bool status)
    {
        vibrationStatus = status;
        Observer.Noti(conststring.UPDATESOUNDVIBRATION);
    }
    public bool GetVibrationStatus()
    {
        return vibrationStatus;
    }
    public bool GetSoundStatus()
    {
        return soundStatus;
    }
    public void SetCurrentIDLevel(int iDLevel)
    {
        Observer.Noti(conststring.UPDATEUI);
        currentIDLevel = iDLevel;
    }
    public int GetCurrentIDLevel()
    {
        return currentIDLevel;
    }
    public int NextCurrentIDLevel()
    {
        currentIDLevel++;
        if(currentIDLevel > 5)
        {
            currentIDLevel = 1;
        }
        Observer.Noti(conststring.UPDATEUI);
        return currentIDLevel;
    }
    public void SetCurrentCoin(int coin)
    {
        currentCoin = coin;
    }
    public int GetCurrentCoin()
    {
        return currentCoin;
    }
    public void SetCurrentSkinName(string skinName)
    {
        currentSkinName = skinName;
    }
    public string GetCurrentSkinName()
    {
        return currentSkinName;
    }
    public void AddCoin(int amount)
    {
        currentCoin += amount;
        Observer.Noti(conststring.UPDATEUI);
    }
    public void SubtractCoin(int amount)
    {
        currentCoin -= amount;
        Observer.Noti(conststring.UPDATEUI);
    }
    public bool HasEnoughCoin(int amount)
    {
        return currentCoin >= amount;
    }
    public string CurrentEnviromentSceneName()
    {
        return "Level"+ currentIDLevel.ToString();
    }
    public int GetCurrentGem()
    {
        return currentGem;
    }
    public void SetCurrentGem(int gem)
    {
        currentGem = gem;
        Observer.Noti(conststring.UPDATEUI);
    }
    public void AddGem(int amount)
    {
        currentGem += amount;
        Observer.Noti(conststring.UPDATEUI);
    }
    public void SubtractGem(int amount)
    {
        currentGem -= amount;
        Observer.Noti(conststring.UPDATEUI);
    }
    public bool HasEnoughGem(int amount)
    {
        return currentGem >= amount;
    }

    internal DynamicData DeepCopy()
    {
        return new DynamicData
        {
            currentIDLevel = this.currentIDLevel,
            currentCoin = this.currentCoin,
            currentGem = this.currentGem,
            currentSkinName = this.currentSkinName,
            soundStatus = this.soundStatus,
            vibrationStatus = this.vibrationStatus
        };
    }
}
