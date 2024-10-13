using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInPlay : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled= false;
    }
}
