using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLightUp : MonoBehaviour {

    public Light Light;
    public Transform LightPosition;
    public bool IsActive = false;    

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !IsActive)
        {
            int randomColor = Random.Range(1, 4);

            switch (randomColor)
            {
                case 1:
                    Light.color = Color.red;
                    break;
                case 2:
                    Light.color = Color.green;
                    break;
                case 3:
                    Light.color = Color.blue;
                    break;
                default:
                    break;
            }

            Instantiate(Light, LightPosition);
            IsActive = true;
            GameManager.GM.Collect(1);
            GameManager.GM.CoinSound.Play();
        }
    }
}
