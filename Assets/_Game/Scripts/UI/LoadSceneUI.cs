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
    [SerializeField] private GameObject dim;
    bool complete = false;
    private void Start()
    {
        Observer.AddListener(conststring.DONELOADSCENEASYNC, ToCompleteProgress);
        //Observer.AddListener(conststring.DONELOADSCENEASYNC,ChangeToHome);
    }

    private void ChangeToHome()
    {
        Debug.Log("alo");
        Observer.RemoveListener(conststring.DONELOADSCENEASYNC, ChangeToHome);
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        complete = false;
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InQuint);
        Invoke(nameof(BeginProgress), 0.5f);
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
        Debug.Log("to complete");
        complete = true;
        loadingProgress.DOFillAmount(0f, 3f).SetEase(Ease.InOutQuint)
            .OnComplete(() =>
            {
                //dim.SetActive(false);
                canvasGroup.DOFade(0f, 0.4f).SetEase(Ease.OutQuint).OnComplete(()=> {
                    //dim.SetActive(false);
                    dim.GetComponent<Image>().DOFade(0, 0.3f).OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    }).SetEase(Ease.Linear);
                });
            });
    }
}
