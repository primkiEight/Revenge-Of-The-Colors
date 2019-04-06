using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorNewFloor : MonoBehaviour
{
    public GameObject NextFloor;
    public bool DeactiveMe = true;

    public void OnTriggerStay(Collider other)
    {
        if (NextFloor == null)
            return;
        else if (other.tag == "Player")
        {
            if (!NextFloor.active)
                NextFloor.SetActive(true);
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && DeactiveMe)
        {
            if (gameObject.active)
                gameObject.SetActive(false);
        }
    }
}