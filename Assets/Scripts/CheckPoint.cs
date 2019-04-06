using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public Transform NewRespawnPosition;
    public GameObject ActivationLight;    

    private void Start()
    {
        ActivationLight.GetComponentInChildren<Light>().enabled = false;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.GM.SetActiveCheckPoint(gameObject);
            
            HealthManager healthManager = other.GetComponent<HealthManager>();
            
            if (healthManager)
            {
                healthManager.SetRespawnPosition(NewRespawnPosition.position);
            }
        }
    }

}
