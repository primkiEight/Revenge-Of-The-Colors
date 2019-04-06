using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour {

    public enum OnDeathAction
    {
        ReloadLevel,
        LoseLife,
        LoseLifeAndReloadLevel,
        Nothing
    }

    public OnDeathAction DeathOnDeathZone;

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Player")
        {
            switch (DeathOnDeathZone)
            {
                case OnDeathAction.ReloadLevel:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;

                case OnDeathAction.LoseLife:
                    other.gameObject.GetComponent<HealthManager>().CurrentHP = -1;
                    break;

                case OnDeathAction.LoseLifeAndReloadLevel:
                    other.gameObject.GetComponent<HealthManager>().CurrentHP = -1;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;

                case OnDeathAction.Nothing:
                    break;

                default:
                    break;
            }

        } else
        {
            Destroy(other.gameObject); //moguće je dodati i vrijeme nakon kojeg će objekt biti uništen
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {

        }
        else
        {
            Destroy(other.gameObject); //moguće je dodati i vrijeme nakon kojeg će objekt biti uništen

        }
    }*/
}
