using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Player1 playerInstance;
    [SerializeField] private Transform myTransform;
    [SerializeField] private float speed = 100;
    [SerializeField] private float paramForScale = 70f;
    [SerializeField] private Vector3 offset = new Vector3();
    private void Start()
    {
        myTransform = this.gameObject.transform;
        playerInstance = player.gameObject.GetComponent<Player1>();
    }
    void Update()
    {
        myTransform.position = Vector3.Lerp
        (
            myTransform.position,
            player.position + offset * ((float)playerInstance.GetBrickInBody() / paramForScale + 1f),
            speed * Time.deltaTime
        );
    }
}
