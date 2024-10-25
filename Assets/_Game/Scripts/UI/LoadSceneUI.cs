using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class LoadSceneUI : MonoBehaviour
{
    [SerializeField] private Image loadingProgress;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image dim1;
    [SerializeField] private Image dim2;
    bool complete = false;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Observer.AddListener(conststring.DONELOADSCENEASYNC, ToCompleteProgress);
        Observer.AddListener(conststring.DONELOADLEVEL, ToCompleteProgress);
        Observer.AddListener(conststring.NEXTLEVEL,ChangeToHome);
        Observer.AddListener(conststring.RELOADLEVEL,ChangeToHome);
        InitLoad();
    }
    private void ChangeToHome()
    {
        gameObject.SetActive(true);
        loadingProgress.fillAmount = 1;
        dim1.DOFade(1, 0.3f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                canvasGroup.alpha = 1f;   
                InitLoad();
            });
    }
    private void InitLoad()
    {
        complete = false;
        canvasGroup.alpha = 1f;
        dim2.DOFade(0,0.4f).SetEase(Ease.Linear);
        Invoke(nameof(BeginProgress), 0.4f);
    }
    private void BeginProgress()
    {
        if (complete) return;
        loadingProgress.DOFillAmount(0.7f, 7f).SetEase(Ease.OutQuad);
        Invoke(nameof(WaitDoneProgress), 5f);
    }
    private void WaitDoneProgress()
    {
        if (complete) return;
        loadingProgress.DOFillAmount(0f, 5f).SetEase(Ease.Linear);
    }
    private void ToCompleteProgress()
    {
        complete = true;
        loadingProgress.DOFillAmount(0f, 3f).SetEase(Ease.InOutQuint)
            .OnComplete(() =>
            {
                dim2.DOFade(1, 0.4f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    canvasGroup.alpha = 0f;
                    dim1.DOFade(0, 0.3f).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    }).SetEase(Ease.Linear);
                }).SetEase(Ease.Linear);
            });
    }
}
