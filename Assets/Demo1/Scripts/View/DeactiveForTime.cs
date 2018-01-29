using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveForTime: MonoBehaviour {

    private void OnEnable()
    {
        Invoke("Deactive", 3);

    }

    private void Deactive()
    {
        this.gameObject.SetActive(false);
    }
}
