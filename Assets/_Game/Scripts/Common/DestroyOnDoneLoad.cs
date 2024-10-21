using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDoneLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Observer.AddListener(conststring.DONELOADSCENEASYNC, () =>
        {
            Destroy(gameObject);
        });
    }
}
