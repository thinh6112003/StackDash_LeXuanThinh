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
    [SerializeField] private CameraType currentCameraType;
    private float followRunCameraDistanceOrigin;
    private float followReadyCameraDistanceOrigin;
    private float followEndGameCameraDistanceOrigin;
    private void Awake()
    {
    }
    private void Start()
    {
        SetRefCamera(ref cameraFollowReadyFT, cameraFollowReady, ref followReadyCameraDistanceOrigin);
        SetRefCamera(ref cameraFollowRunFT,cameraFollowRun, ref followRunCameraDistanceOrigin);
        SetRefCamera(ref cameraFollowEndGameFT,cameraFollowEndGame, ref followEndGameCameraDistanceOrigin);
        SetCamera(CameraType.FollowReady);
        Observer.AddListener(conststring.CHANGECAMFOLLOWREADY, SetCameraFollowReady);
        Observer.AddListener(conststring.CHANGECAMFOLLOWRUN, SetCameraFollowRun);
        Observer.AddListener(conststring.CHANGECAMFOLLOWENDGAME,SetCameraFollowEndGame );
        Observer.AddListener(conststring.UPDATECAMERA, UpdateCamera);
        Observer.AddListener(conststring.DONELOADLEVEL, InitGame);
        
    }
    private void Update()
    {
        listenerDoneChangeCam();
    }
    private void SetRefCamera(ref CinemachineFramingTransposer cameraFT, CinemachineVirtualCamera cinemachineVirtualCamera , ref float mDistanceOrigin)
    {
        cameraFT = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        mDistanceOrigin = cameraFT.m_CameraDistance;
    }
    private void listenerDoneChangeCam()
    {
        if( currentCameraBrain.IsBlending == false && isBlendingPrevios== true && currentCameraType== CameraType.FollowRun)
        {
            Observer.Noti(conststring.DONECHANGECAM);
        }
        isBlendingPrevios = currentCameraBrain.IsBlending;
    }
    private void InitGame()
    {
        cameraFollowEndGameFT.m_CameraDistance = followEndGameCameraDistanceOrigin;
        cameraFollowReadyFT.m_CameraDistance = followReadyCameraDistanceOrigin;
        cameraFollowRunFT.m_CameraDistance = followRunCameraDistanceOrigin;
        SetCamera(CameraType.FollowReady);
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
        Debug.Log("UpdateCamera");
        playerInstance.GetBrickInBody();
        float scaleDistanceCamera = (float)playerInstance.GetBrickInBody() / paramForScale + 1f;
        currentCameraT.m_CameraDistance =  distanceOrigin* scaleDistanceCamera;
    }
    public void ChangeCamera(CinemachineVirtualCamera cameraSet, CinemachineFramingTransposer cameraSetT)
    {
        if (currentCamera != cameraSet)
        {
            if(currentCamera != null) currentCamera.Priority = 0;
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
                currentCameraType = CameraType.FollowRun;
                ChangeCamera(cameraFollowRun,cameraFollowRunFT);
                break;
            case CameraType.FollowReady:
                currentCameraType = CameraType.FollowReady;
                ChangeCamera(cameraFollowReady,cameraFollowReadyFT);
                break;
            case CameraType.FollowEndGane:
                currentCameraType= CameraType.FollowEndGane;
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
