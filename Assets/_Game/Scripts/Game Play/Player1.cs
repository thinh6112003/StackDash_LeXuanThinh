using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player1 : MonoBehaviour
{
    [SerializeField] private List<Transform> brickContainer= new List<Transform>();
    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform playerModel;
    [SerializeField] private Transform brickContainerTf;
    [SerializeField] private LastPiece planeEndGame;
    [SerializeField] private Transform stairsTf;
    [SerializeField] private float speed= 10;
    private List<Transform> listBridgePointCurrent = new List<Transform>();
    private Bridge bridgeCurrent;
    private LastPiece_lineStraight LastPieceLSCurrent;
    private Transform playerTf;
    private Vector3 target;
    private Vector3 beginPos;
    private Vector3 brickHeight = new Vector3(0f, 0.25f, 0f);
    private Vector3 pointCheckOffset = new Vector3(0f, 0.25f, 0f);
    private Vector3 brickOnBridgeOffset = new Vector3(0f, 0.28f, 0f);
    private string lastAnimName = conststring.SURFING;
    private string currentAnimName = conststring.IDLE;
    private float half = 0.5f;
    private float oneBrick = 1f;
    private float oneBridge = 1.41f;
    private int countBridgeInBody = 0;
    private const int wall = 1 << 3;
    private const int moveNext = 1 << 6;
    private const int toSlope = 1 << 7;
    private const int slope = 1 << 8;
    private const int bridgeBridge = 1 << 10;
    private const int ground = 1 << 11;
    private const int stair = 1 << 12;
    private const int lastPiece = 1 << 13;
    private const int endGame = 1 << 14;
    private int indexInBridge;
    private bool isMove = false;
    private bool isOnBridge = false;
    private bool isMouseDown = false;
    private bool directOnBridge = false;// false: negative, True: positive 
    private bool isOutBeginBridge = false;
    private void Start()
    {
        //Time.timeScale = 0.1f;
        playerTf = this.transform;
    }
    void Update()
    {
        //Debug.Log("is on bridge: " + isOnBridge);
        //Debug.Log("IS  ismove:" + isMove);
        //Debug.Log("IS  isonbridge:" + isOutBeginBridge);
        OnMove();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == conststring.BRICK) PickBrick(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == conststring.ENTRANCEBRIDGE|| other.gameObject.tag== conststring.ENDBRIDGE)
        {
            if (isOutBeginBridge) isOutBeginBridge = false;
        }
    }
    public int GetBrickInBody()
    {
        return brickContainer.Count;
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public void SetAnimation(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.IDLE:
                ChangeAnim(conststring.IDLE);
                break;
            case AnimationType.MOVE:
                ChangeAnim(conststring.MOVE);
                break;
            case AnimationType.SURFING:
                ChangeAnim(conststring.SURFING);
                break;
            case AnimationType.STACKREACTION:
                ChangeAnim(conststring.STACKREACTION);
                currentAnimName = conststring.IDLE;
                break;
        }
    }
    private void OnMove()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
        if (!isMove)
        {
            if (isMouseDown == false && Input.GetMouseButtonDown(0))
            {
                beginPos = Input.mousePosition;
                isMouseDown = true;
            }
            if (isMouseDown||Input.anyKeyDown)
            {
                Vector3 endPos = Input.mousePosition;
                if (isOnBridge)
                {
                    HandleMoveOnBridge(new Vector3(endPos.x - beginPos.x, 0, endPos.y - beginPos.y));
                    return;
                }
                if (endPos.x - beginPos.x >= 20) DetermineTarget(new Vector3(0.5f, 0, 0));
                else if (endPos.x - beginPos.x <= -20) DetermineTarget(new Vector3(-0.5f, 0, 0));
                else if (endPos.y - beginPos.y >= 20) DetermineTarget(new Vector3(0, 0, 0.5f));
                else if (endPos.y - beginPos.y <= -20) DetermineTarget(new Vector3(0, 0, -0.5f));
                if (Input.GetKeyDown(KeyCode.RightArrow)) DetermineTarget(new Vector3(0.5f, 0, 0));
                else if (Input.GetKeyDown(KeyCode.LeftArrow)) DetermineTarget(new Vector3(-0.5f, 0, 0));
                else if (Input.GetKeyDown(KeyCode.UpArrow)) DetermineTarget(new Vector3(0, 0, 0.5f));
                else if (Input.GetKeyDown(KeyCode.DownArrow)) DetermineTarget(new Vector3(0, 0, -0.5f));
            }
        }
    }
    private void DetermineTarget(Vector3 direc, bool isEndBridge=false)
    {
        Vector3 playerPos = playerTf.position;
        RaycastHit hit;
        if (isOutBeginBridge && !Physics.Raycast(playerPos + pointCheckOffset*4f + direc * 3f, Vector3.down, out hit, 5, ground)) return;
        if (Physics.Raycast(playerPos + pointCheckOffset * 10f + direc * 2f, Vector3.down, out hit, 5, wall)) return;

        if (Physics.Raycast(playerPos + pointCheckOffset + direc * 1.2f, direc, out hit, 50f))
        {
            int layer = 1<< (hit.transform.gameObject.layer);
            if (!(layer == toSlope || layer == moveNext || layer == wall|| layer == stair|| layer== lastPiece)) return;
            Quaternion targetRotation = Quaternion.LookRotation(direc);
            playerModel.DORotateQuaternion(targetRotation, 0.15f);
            SetAnimation(AnimationType.MOVE);
            isMove = true;
            float time = ((Mathf.Abs(hit.point.x - playerPos.x) > 0.5f) ? Mathf.Abs(hit.point.x - playerPos.x) 
                : Mathf.Abs(hit.point.z - playerPos.z)) /speed;
            switch (layer)
            {
                case wall:
                case moveNext:
                    HandleMoveToWall_MoveNext(hit, direc, isEndBridge, time, layer);
                    break;
                case toSlope:
                    HandleMoveToSlope(hit, playerPos, direc, isEndBridge, time);
                    break;
                case stair:
                    HandeMoveToStair(hit, playerPos, direc, isEndBridge, time);
                    break;
                case lastPiece:
                    MoveLastPiece();
                    break;
                case endGame:
                    HandeMoveToEndGame(hit, playerPos, direc, isEndBridge, time);
                    break;
            }
        }
    }

        private void HandeMoveToEndGame(RaycastHit hit, Vector3 playerPos, Vector3 direc, bool isEndBridge, float time)
        {
            throw new NotImplementedException();
        }
        private void HandeMoveInLastPiece(RaycastHit hit, Vector3 playerPos, Vector3 direc, bool isEndBridge, float time)
        {

        }
        private void HandeMoveToStair(RaycastHit hit, Vector3 playerPos, Vector3 direc, bool isEndBridge, float time)
        {
            Debug.Log("to stair");
            int beginNumberBrick = brickContainer.Count;
            playerTf.DOMove(new Vector3(hit.point.x, playerTf.position.y, hit.point.z)- direc , time)
                .SetEase(Ease.Linear)
                .OnComplete(() => {
                    if (brickContainer.Count < 4) {
                        Debug.Log("End Game in stair");
                        return;
                    };
                    Debug.Log("complete to stair");
                    for(int i= 1; i<= 4; i++)
                    {
                        brickContainer[brickContainer.Count-1].parent = stairsTf;
                        brickContainer.RemoveAt(brickContainer.Count-1);
                    }
                    translateSomeBrickHeight(brickContainerTf, -4f);
                    translateSomeBrickHeight(playerModel, -4f);
                    translateSomeBrickHeight(playerTf, 4f);
                    DetermineTarget(direc, isEndBridge);
                });
        }
            private void translateSomeBrickHeight(Transform objectToTranslate, float number)
            {
                objectToTranslate.position += number * brickHeight;
            }
        private void HandleMoveOnBridge(Vector3 direc)
        {
            float goc = Vector3.Angle(direc, Vector3.forward);
            if(goc> 90f)
            {
                indexInBridge--;
                isMove = true;
                directOnBridge = !directOnBridge;
                //MoveNextBridgePoint();
            }
        }
        private void HandleMoveToWall_MoveNext(RaycastHit hit, Vector3 direc, bool isEndBridge, float time,int layer)
        {
            int beginNumberBrick = brickContainer.Count;
            Vector3 offsetTarget = layer == wall ? direc : Vector3.zero;
            playerTf.DOMove(new Vector3(hit.point.x, playerTf.position.y, hit.point.z) - offsetTarget, time)
                        .SetEase(Ease.Linear)
                        .OnComplete(() => {
                            if (isEndBridge) 
                            { 
                                isOnBridge = false;
                                bridgeCurrent.TurnOnCheckInOut();
                            }
                            if (!isOnBridge) isMove = false;
                            if (layer == moveNext && !isOutBeginBridge)
                            {
                                DetermineTarget(direc, isEndBridge);
                            }
                            if (layer == wall) {
                                if(brickContainer.Count > beginNumberBrick)
                                {
                                    SetAnimation(AnimationType.STACKREACTION);
                                    currentAnimName = conststring.IDLE;
                                }
                                else
                                {
                                    SetAnimation(AnimationType.IDLE);
                                }
                            }

                            beginPos = Input.mousePosition;
                        });
        }
        private void HandleMoveToSlope(RaycastHit hit, Vector3 playerPos,Vector3 direc, bool isEndBridge, float time)
        {
            Physics.Raycast(playerPos + pointCheckOffset, direc, out hit, 20f, toSlope);
            playerTf.DOMove(hit.transform.position, time)
                .SetEase(Ease.Linear)
                .OnComplete(() => {
                    if (isEndBridge) isOnBridge = false;
                    if (!isOnBridge) isMove = false;
                    if (isOutBeginBridge) isOutBeginBridge = false;
                    DetermineTarget(direc, isEndBridge);
                });
        }
    private void PickBrick(GameObject brick)
    {
        brick.tag = conststring.UNTAGGED;
        brick.transform.parent = brickContainerTf;
        brickContainer.Add(brick.transform);
        countBridgeInBody++;
        int cout = countBridgeInBody;
        brick.transform.localPosition = -countBridgeInBody*brickHeight;
        Vector3 targetLocalPosition = countBridgeInBody * brickHeight;
        playerModel.DOLocalMoveY(countBridgeInBody*brickHeight.y, oneBrick / (speed*3f))
            .SetEase(Ease.Linear)
            .OnComplete(()=>
            {
                playerModel.localPosition = targetLocalPosition;
            });
        brickContainerTf.DOLocalMoveY(targetLocalPosition.y, oneBrick / (speed*3f))
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                brickContainerTf.localPosition = targetLocalPosition;
            });
    }

    private void MoveLastPiece(bool direct= true)
    {
        //Time.timeScale = 0.1f;
        if(brickContainer.Count==0)
        {
            isOutBeginBridge = true;
            isOnBridge = false;
            return;
        }
        SetAnimation(AnimationType.SURFING);
        isMove = true;
        DOTween.KillAll();
        MoveNextLastPiecePoint();
    }
        private void MoveNextLastPiecePoint()
        {
            Transform nextLayBrickPoint = planeEndGame.GetNextLayBrickPoint();
            if (nextLayBrickPoint == null)
            {
                Debug.Log("den dich");
                HandleEndBridgeLastPiece();
                return;
            }
            if (brickContainer.Count == 0)
            {
                Debug.Log("het tien");
                isOutBeginBridge = true;
                isOnBridge = false;
                return;
            }
            Vector3 posPoint = nextLayBrickPoint.position;
            Transform brickTransformOut = LayBrickModel();
            playerTf.DOMove(new Vector3(posPoint.x, playerTf.position.y, posPoint.z), oneBridge / speed).SetEase(Ease.Linear)
                .SetEase(Ease.Linear)
                .OnComplete(() => {
                    LayBrickLastPieceView(brickTransformOut, nextLayBrickPoint);
                    MoveNextLastPiecePoint();
                });
        }
        private void HandleEndBridgeLastPiece()
        {
            isMove = true;
            DetermineTarget(playerTf.forward);
        }
            private void LayBrickLastPieceView(Transform brickTransform, Transform pointLayBrick)
            {
                brickTransform.parent = pointLayBrick;
                brickTransform.position = pointLayBrick.position-brickHeight;
                countBridgeInBody--;
                Vector3 brickContainerLcPos = brickContainerTf.position;
                Vector3 playerModelLcPos = playerModel.position;
                brickContainerTf.position = new Vector3(brickContainerLcPos.x, brickContainerLcPos.y - brickHeight.y, brickContainerLcPos.z);
                playerModel.position = new Vector3(playerModelLcPos.x, playerModelLcPos.y - brickHeight.y, playerModelLcPos.z);
            }
            private Transform LayBrickModel()
            {
                int indexBrick = brickContainer.Count - 1;
                Transform brickTransform = brickContainer[indexBrick];
                brickContainer.RemoveAt(indexBrick);
                return brickTransform;
            }
        private void HandleNextPointIsBrick(int index, Vector3 posPoint)
        {
            indexInBridge += directOnBridge ? 1 : -1;
            playerTf.DOMove(new Vector3(posPoint.x, playerTf.position.y, posPoint.z), oneBridge / speed).SetEase(Ease.Linear)
                .SetEase(Ease.Linear)
                .OnComplete(() => {
                    //MoveNextBridgePoint();
                });
        }
}