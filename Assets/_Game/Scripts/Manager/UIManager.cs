using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backHomeButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject homeSceneUI;
    [SerializeField] private GameObject gamePlaySceneUI;
    [SerializeField] private GameObject finishLevelSceneUI;
    [SerializeField] private GameObject currentSceneUI;
    [SerializeField] private GameObject settingPopup;
    [SerializeField] private TMP_Text level_Home_Txt;
    [SerializeField] private TMP_Text coin_Home_Txt;
    [SerializeField] private TMP_Text gem_Home_Txt;
    [SerializeField] private TMP_Text level_GP_Txt;
    [SerializeField] private TMP_Text score_GP_Txt;
    [SerializeField] private TMP_Text coin_EG_Txt;
    [SerializeField] private TMP_Text gem_EG_Txt;
    [SerializeField] private TMP_Text score_EG_Txt;
    [SerializeField] private TMP_Text earned_EG_Txt;

    private void Start()
    {
        currentSceneUI = homeSceneUI;
        playButton.onClick.AddListener(OnClickPlayButton);
        continueButton.onClick.AddListener(OnClickContinueButton);
        backHomeButton.onClick.AddListener(OnClickBackHomeButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
        Observer.AddListener(conststring.FINISHGAME, SetFinishLevel);
        Observer.AddListener(conststring.UPDATEUI, UpdateTxtUI);
        Observer.AddListener(conststring.DONELOADLEVEL, () => {
            SetUIScene(SceneUIType.HomeScene);
        });
        UpdateTxtUI();
    }

    private void OnClickSettingButton()
    {
        AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        settingPopup.SetActive(true);
    }
    private void UpdateTxtUI()
    {
        DynamicData dynamicdata =  DataManager.Instance.dynamicData;
        level_Home_Txt.text = "Level " + dynamicdata.currentIDLevel;
        coin_Home_Txt.text = dynamicdata.GetCurrentCoin().ToString();
        gem_Home_Txt.text = dynamicdata.GetCurrentGem().ToString();

        level_GP_Txt.text = "Level " + dynamicdata.currentIDLevel;
        score_GP_Txt.text = DataManager.Instance.GetScore().ToString();
        
        coin_EG_Txt.text = dynamicdata.GetCurrentCoin().ToString();
        gem_EG_Txt.text = dynamicdata.GetCurrentGem().ToString();
        score_EG_Txt.text = DataManager.Instance.GetScore().ToString();
        earned_EG_Txt.text = DataManager.Instance.GetEarned().ToString();
    }
    private void OnClickBackHomeButton()
    {
        AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        Observer.Noti(conststring.RELOADLEVEL);
    }

    private void OnClickContinueButton()
    {
        AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        Observer.Noti(conststring.NEXTLEVEL);
    }

    private void OnClickPlayButton()
    {
        AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        Observer.Noti(conststring.CHANGECAMFOLLOWRUN);
        SetUIScene(SceneUIType.GamePlayScene);
    }
    private void SetFinishLevel()
    {
        SetUIScene(SceneUIType.FinishLevelScene);
    }
    public void ChangeSceneUI(GameObject sceneUISet)
    {
        if (currentSceneUI != sceneUISet)
        {
            currentSceneUI.SetActive(false);
            currentSceneUI = sceneUISet;
            currentSceneUI.SetActive(true);
        }
    }
    public void SetUIScene(SceneUIType sceneUIType)
    {
        switch (sceneUIType)
        {
            case SceneUIType.HomeScene:
                ChangeSceneUI(homeSceneUI);
                break;
            case SceneUIType.GamePlayScene:
                ChangeSceneUI(gamePlaySceneUI);
                break;
            case SceneUIType.FinishLevelScene:
                ChangeSceneUI(finishLevelSceneUI);
                break;
        }
    }
    public enum SceneUIType
    {
        HomeScene,
        GamePlayScene,
        FinishLevelScene
    }
}
