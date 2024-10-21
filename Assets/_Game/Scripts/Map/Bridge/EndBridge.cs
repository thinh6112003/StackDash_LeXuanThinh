using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBridge : MonoBehaviour
{
    [SerializeField] private Bridge parent;
    public Bridge Parent()
    {
        return parent;
    }
}
