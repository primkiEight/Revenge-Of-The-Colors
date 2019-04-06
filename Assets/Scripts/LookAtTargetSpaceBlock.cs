using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetSpaceBlock : MonoBehaviour {

    private void Start()
    {
        float x = Random.Range(0f, 90f);
        float y = Random.Range(0f, 90f);
        float z = Random.Range(0f, 90f);

        transform.localEulerAngles = new Vector3(x, y, z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.LookAt(other.transform);
        }
    }
}
