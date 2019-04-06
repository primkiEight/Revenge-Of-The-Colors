using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideFloor : MonoBehaviour {

    public float TriggerTimer;
    public float DestroyTimer;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Invoke("FallIntoOblivion", TriggerTimer);
        }
    }

    private void FallIntoOblivion()
    {
        GetComponent<Rigidbody>().useGravity = true;
        Invoke("DestroyOverTime", DestroyTimer);
    }

    private void DestroyOverTime()
    {
        Destroy(gameObject);
    }
}
