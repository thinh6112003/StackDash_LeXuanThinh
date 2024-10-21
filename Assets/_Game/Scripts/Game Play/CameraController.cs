using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player playerInstance;
    [SerializeField] private float paramForScale=70f;
    [SerializeField] private float distanceOrigin= 20f;
    [SerializeField] private bool isBlendingPrevios= false;
    [SerializeField] private CinemachineVirtualCamera cameraFollowReady;
    [SerializeField] private CinemachineVirtualCamera cameraFollowRun;
    [SerializeField] private CinemachineVirtualCamera cameraFollowEndGame;
    [SerializeField] private CinemachineVirtualCamera currentCamera;
    [SerializeField] private CinemachineBrain currentCameraBrain;
    [SerializeField] private CinemachineFramingTransposer cameraFollowRunFT;
    [SerializeField] private CinemachineFramingTransposer cameraFollowReadyFT;
    [SerializeField] private CinemachineFramingTransposer cameraFollowEndGameFT;
    [SerializeField] private CinemachineFramingTransposer currentCameraT;
    private void Start()
    {
        cameraFollowReadyFT = cameraFollowReady.GetCinemachineComponent<CinemachineFramingTransposer>();
        cameraFollowRunFT = cameraFollowRun.GetCinemachineComponent<CinemachineFramingTransposer>();
        cameraFollowEndGameFT = cameraFollowEndGame.GetCinemachineComponent<CinemachineFramingTransposer>();
        currentCamera = cameraFollowReady;
        currentCameraT = cameraFollowReadyFT;
        Observer.AddListener(conststring.CHANGECAMFOLLOWREADY, SetCameraFollowReady);
        Observer.AddListener(conststring.CHANGECAMFOLLOWRUN, SetCameraFollowRun);
        Observer.AddListener(conststring.CHANGECAMFOLLOWENDGAME,SetCameraFollowEndGame );
        Observer.AddListener(conststring.UPDATECAMERA, UpdateCamera);
        Observer.AddListener(conststring.DONELOADNEXTLEVEL, ()=> { 
            StartCoroutine(SetZeroDampingOneFrame());
        });
        
    }
    private void Update()
    {
        listenerDoneChangeCam();
    }
    private void listenerDoneChangeCam()
    {
        if( currentCameraBrain.IsBlending == false && isBlendingPrevios== true)
        {
            Observer.Noti(conststring.DONECHANGECAM);
        }
        isBlendingPrevios = currentCameraBrain.IsBlending;
    }
    public IEnumerator SetZeroDampingOneFrame()
    {
        SetZeroDamping();
        yield return null;
        SetDefaultDamping();
    }
    private void SetZeroDamping()
    {
        currentCameraT.m_XDamping = 0;
        currentCameraT.m_YDamping = 0;
        currentCameraT.m_ZDamping = 0;
    }
    public void SetDefaultDamping()
    {
        currentCameraT.m_XDamping = 1;
        currentCameraT.m_YDamping = 1;
        currentCameraT.m_ZDamping = 1;
    }
    public void SetCameraFollowReady()
    {
        SetCamera(CameraType.FollowReady);
    }
    public void SetCameraFollowRun()
    {
        SetCamera(CameraType.FollowRun);
    }
    public void SetCameraFollowEndGame()
    {
        SetCamera(CameraType.FollowEndGane);
    }
    private void UpdateCamera()
    {
        playerInstance.GetBrickInBody();
        float scaleDistanceCamera = (float)playerInstance.GetBrickInBody() / paramForScale + 1f;
        currentCameraT.m_CameraDistance =  distanceOrigin* scaleDistanceCamera;
    }
    public void ChangeCamera(CinemachineVirtualCamera cameraSet, CinemachineFramingTransposer cameraSetT)
    {
        if (currentCamera != cameraSet)
        {
            currentCamera.Priority = 0;
            currentCamera = cameraSet;
            currentCameraT = cameraSetT;
            currentCamera.Priority = 1;
        }
    }
    public void SetCamera(CameraType cameraType)
    {
        switch (cameraType)
        {
            case CameraType.FollowRun:
                ChangeCamera(cameraFollowRun,cameraFollowRunFT);
                break;
            case CameraType.FollowReady:
                ChangeCamera(cameraFollowReady,cameraFollowReadyFT);
                break;
            case CameraType.FollowEndGane:
                ChangeCamera(cameraFollowEndGame, cameraFollowEndGameFT);
                break;
        }
    }

}
public enum CameraType
{
    FollowRun,
    FollowReady,
    FollowEndGane
}
