using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour {

    public bool IsActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !IsActive)
            IsActive = true;
    }
}
