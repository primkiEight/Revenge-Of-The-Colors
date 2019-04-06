using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTrigger : MonoBehaviour {

    public bool IsActive = true;

    public Light TriggerLight;
    public Light TargetLight;

    public int ColorNumber = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && IsActive)
        {
            ColorNumber++;
        }

        if (other.tag == "Player" && !IsActive)
        {
            ColorNumber = 0;
        }
    }
    private void Update()
    {
        switch (ColorNumber)
        {
            case 1:
                TriggerLight.color = Color.grey;
                TargetLight.color = Color.red;
                break;

            case 2:
                TriggerLight.color = Color.red;
                TargetLight.color = Color.yellow;
                break;

            case 3:
                TriggerLight.color = Color.yellow;
                TargetLight.color = Color.green;
                break;

            case 4:
                TriggerLight.color = Color.green;
                TargetLight.color = Color.cyan;
                break;

            case 5:
                TriggerLight.color = Color.cyan;
                TargetLight.color = Color.blue;
                break;

            case 6:
                TriggerLight.color = Color.blue;
                TargetLight.color = Color.magenta;
                break;

            case 7:
                TriggerLight.color = Color.magenta;
                TargetLight.color = Color.white;
                break;

            case 8:
                TriggerLight.color = Color.white;
                TargetLight.color = Color.black;
                ColorNumber = 0;
                //IsActive = false;
                break;

            default:
                TriggerLight.color = Color.black;
                TargetLight.color = Color.grey;
                break;
        }
    }

}
