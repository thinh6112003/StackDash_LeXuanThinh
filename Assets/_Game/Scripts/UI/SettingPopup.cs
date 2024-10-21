using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{

    [SerializeField] private Button vibarationBtn;
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private GameObject vibrationOn;
    [SerializeField] private GameObject vibrationOff;
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;
    [SerializeField] private bool vibrationStatus = true;
    [SerializeField] private bool soundStatus = true;
    private void Start()
    {
        soundBtn.onClick.AddListener(OnClickSoundBtn);
        vibarationBtn.onClick.AddListener(OnClickVibrationBtn);
        closeBtn.onClick.AddListener(OnClickCloseBtn);
    }
    private void OnClickCloseBtn()
    {
        this.gameObject.SetActive(false);
    }

    private void OnClickVibrationBtn()
    {
        vibrationStatus = !vibrationStatus;
        vibrationOn.SetActive(vibrationStatus);
        vibrationOff.SetActive(!vibrationStatus);
        DataManager.Instance.dynamicData.SetVibrationStatus(vibrationStatus);
    }

    private void OnClickSoundBtn()
    {
        soundStatus = !soundStatus;
        soundOn.SetActive(soundStatus);
        soundOff.SetActive(!soundStatus);
        DataManager.Instance.dynamicData.SetActiveSoundStatus(soundStatus);
    }
}
