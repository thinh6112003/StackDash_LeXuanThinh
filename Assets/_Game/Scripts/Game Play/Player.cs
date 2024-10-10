using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform playerModel;
    [SerializeField] Transform brickContainerTf;
    [SerializeField] private float speed= 10;
    //private tran
    private List<Transform> listBridgePointCurrent = new List<Transform>();
    [SerializeField] private List<Transform> brickContainer= new List<Transform>();
    private Bridge bridgeCurrent;
    private Vector3 pointCheckOffset = new Vector3(0f, 0.25f, 0f);
    private Vector3 brickOnBridgeOffset = new Vector3(0f, 0.28f, 0f);
    private Vector3 brickHeight = new Vector3(0f, 0.22f, 0f);
    private Vector3 beginPos;
    private Vector3 target;
    private Transform playerTf;
    private int countBridgeInBody = 0;
    private float oneBridge = 1.41f;
    private float oneBrick = 1f;
    private float half = 0.5f;
    private int wall = 1 << 3;
    private int moveNext = 1 << 6;
    private int toSlope = 1 << 7;
    private int slope = 1 << 8;
    private int bridgeBridge = 1 << 10;
    private int ground = 1 << 11;
    private int indexInBridge;
    private bool isMove = false;
    private bool isOutBeginBridge = false;
    private bool directOnBridge = false;// false: negative, True: positive 
    private bool isOnBridge = false;
    private bool isMouseDown = false;

    private void Start()
    {
        playerTf = player.transform;
    }

    void Update()
    {
        //Debug.Log("IS  ismove:" + isMove);
        //Debug.Log("IS  isonbridge:" + isOutBeginBridge);
        OnMove();
    }

    private void OnMove()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
        if (!isMove&& !isOnBridge)
        {
            if (isMouseDown == false && Input.GetMouseButtonDown(0))
            {
                beginPos = Input.mousePosition;
                isMouseDown = true;
            }
            if (isMouseDown)
            {
                Vector3 endPos = Input.mousePosition;
                if (endPos.x - beginPos.x >= 60) DetermineTarget(new Vector3(0.5f, 0, 0));
                else if (endPos.x - beginPos.x <= -60) DetermineTarget(new Vector3(-0.5f, 0, 0));
                else if (endPos.y - beginPos.y >= 60) DetermineTarget(new Vector3(0, 0, 0.5f));
                else if (endPos.y - beginPos.y <= -60) DetermineTarget(new Vector3(0, 0, -0.5f));
            }
        }
    }

    private void DetermineTarget(Vector3 direc, bool isEndBridge=false)
    {
        Vector3 playerPos = player.transform.position;
        RaycastHit hit;
        if (isOutBeginBridge)
        {
            Debug.Log("is into check");
            if (!Physics.Raycast(playerPos + pointCheckOffset + direc * 3f, Vector3.down, out hit, 5,ground)) return;
        }
        if (Physics.Raycast(playerPos + pointCheckOffset + direc*0.8f, direc, out hit, 50f))
        {
            int layer = 1<< (hit.transform.gameObject.layer);
            if (!(layer == toSlope || layer == moveNext || layer == wall)) return; 
            target = hit.collider.transform.position;
            isMove = true;
            float time = Mathf.Abs(hit.point.z - playerPos.z) / speed;
            if (Mathf.Abs(hit.point.x - playerPos.x) > 0.5f) time = Mathf.Abs(hit.point.x - playerPos.x) / speed;
            Vector3 offsetTarget = layer == wall ? direc : Vector3.zero;
            if(layer== wall || layer == moveNext)
            {
                player.transform.DOMove(new Vector3(hit.point.x, player.transform.position.y, hit.point.z) - offsetTarget, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => {
                        if (isEndBridge) isOnBridge = false;
                        if (!isOnBridge) isMove = false;
                        if (layer == moveNext)
                        {
                            DetermineTarget(direc, isEndBridge);
                        }
                    });
            }
            else if(layer == toSlope)
            {
                Physics.Raycast(playerPos + pointCheckOffset, direc, out hit, 20f,toSlope);
                player.transform.DOMove(hit.transform.position, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => {
                        if (isEndBridge) isOnBridge = false;
                        if (!isOnBridge) isMove = false;
                        DetermineTarget(direc, isEndBridge);
                    });
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == conststring.ENTRANCEBRIDGE)
        {
            if (!isOnBridge)
            {
                bridgeCurrent = other.gameObject.GetComponent<EntranceBridge>().Parent();
                isOnBridge = true;
                MoveBridge(true);
            }
        }
        if (other.gameObject.tag == conststring.ENDBRIDGE)
        {
            if (!isOnBridge)
            {
                bridgeCurrent = other.gameObject.GetComponent<EndBridge>().Parent();
                isOnBridge = true;
                MoveBridge(false);
            }
        }
        if (other.gameObject.tag == conststring.BRICK) PickBrick(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == conststring.ENTRANCEBRIDGE|| other.gameObject.tag== conststring.ENDBRIDGE)
        {
            if (isOutBeginBridge) isOutBeginBridge = false;
        }
    }
    private void PickBrick(GameObject brick)
    {
        brick.tag = conststring.UNTAGGED;
        brick.transform.parent = brickContainerTf;
        brickContainer.Add(brick.transform);
        countBridgeInBody++;
        brick.transform.localPosition = -countBridgeInBody*brickHeight+ brickHeight * 2;

        playerModel.DOLocalMoveY(playerModel.localPosition.y + brickHeight.y, oneBrick / speed).SetEase(Ease.Linear);

        brickContainerTf.DOLocalMoveY(brickContainerTf.localPosition.y + brickHeight.y, oneBrick / speed).SetEase(Ease.Linear);
    }
    private void MoveBridge(bool direct)
    {
        //  Time.timeScale = 0.1f;
        if(brickContainer.Count==0 && !bridgeCurrent.checkBrick(indexInBridge))
        {
            isOutBeginBridge = true;
        }
        directOnBridge = direct;
        isMove = true;
        listBridgePointCurrent = bridgeCurrent.MyBridgePath().ListBridgePoints();
        indexInBridge = directOnBridge ? 0: listBridgePointCurrent.Count-1 ;
        DOTween.KillAll();
        MoveNextBridgePoint();
    }
    public void MoveNextBridgePoint()
    {
        if (indexInBridge == listBridgePointCurrent.Count || indexInBridge < 0)
        {
            HandleEndBridge();
            return;
        }
        Vector3 posPoint = listBridgePointCurrent[indexInBridge].position;
        int index = indexInBridge;
        if (!bridgeCurrent.checkBrick(indexInBridge + 1))
        {
            HandleNextPointNotBrick(index, posPoint);
        }
        else
        {
            HandleNextPointIsBrick(index, posPoint);
        }
    }

    private void HandleNextPointNotBrick(int index, Vector3 posPoint)
    {
        if (brickContainer.Count == 0) 
        {
            HandleIsOutBrickInBody();
            return;
        }
        bridgeCurrent.setBrickStatus(index, true);
        Transform brickTransformOut = LayBrickModel(index);
        indexInBridge += directOnBridge ? 1 : -1;
        player.transform.DOMove(new Vector3(posPoint.x, player.transform.position.y, posPoint.z), oneBridge / speed).SetEase(Ease.Linear)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                LayBrickView(brickTransformOut, index);
                MoveNextBridgePoint();
            });
    }
    private void HandleNextPointIsBrick(int index, Vector3 posPoint)
    {
        Transform brickTransformOut = LayBrickModel(index);
        indexInBridge += directOnBridge ? 1 : -1;
        player.transform.DOMove(new Vector3(posPoint.x, player.transform.position.y, posPoint.z), oneBridge / speed).SetEase(Ease.Linear)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                MoveNextBridgePoint();
            });
    }
    private void HandleIsOutBrickInBody()
    {
        DOTween.KillAll();
        isOnBridge = false;
        isMove = false;
    }
    private void HandleEndBridge()
    {
        isMove = true;
        Vector3 directToPlane;
        if (directOnBridge)
            directToPlane = bridgeCurrent.MyEndBridge().transform.forward;
        else
            directToPlane = -bridgeCurrent.MyEntranceBridge().transform.forward;
        DetermineTarget(directToPlane * half, true);
    }
    private Transform LayBrickModel(int index)
    {
        int indexBrick = brickContainer.Count - 1;
        Transform brickTransform = brickContainer[indexBrick];
        brickContainer.RemoveAt(indexBrick);
        return brickTransform;
    }
    private void LayBrickView(Transform brickTransform, int index)
    {
        brickTransform.parent = listBridgePointCurrent[index];
        brickTransform.position = listBridgePointCurrent[index].position;
        countBridgeInBody--;
        Vector3 brickContainerLcPos = brickContainerTf.position;
        Vector3 playerModelLcPos = playerModel.position;
        brickContainerTf.position = new Vector3(brickContainerLcPos.x, brickContainerLcPos.y - brickHeight.y, brickContainerLcPos.z);
        playerModel.position = new Vector3(playerModelLcPos.x, playerModelLcPos.y - brickHeight.y, playerModelLcPos.z);
    }
}
public static class conststring
{
    public static string BRICK = "Brick";
    public static string LOPEUP = "LopeUP";
    public static string ENDBRIDGE = "EndBridge";
    public static string ENTRANCEBRIDGE = "EntranceBridge";
    public static string BRIDGEBRICK = "BridgeBrick";
    public static string UNTAGGED = "Untagged";
    public static string GROUND = "Ground";
}