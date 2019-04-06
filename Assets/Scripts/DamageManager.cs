using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {

    public float Damage = 10.0f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            HealthManager _healthManager = other.gameObject.GetComponent<HealthManager>();

            if (_healthManager)
            {
                _healthManager.ApplyDamage(Damage);
            }     
        }

        if (other.transform.tag == "Damagable")
        {
            HealthManager _healthManager = other.gameObject.GetComponent<HealthManager>();

            if (_healthManager)
            {
                _healthManager.ApplyDamage(Damage);
            }

            //Svaki put kada se dva neprijatelja s tagom Damagable sudare, player će dobiti 10 bodova
            /*if (other.transform.tag == "Damagable")
                GameManager.GM.Collect(10);
                */
            //Destroy(gameObject);            
        }
    }
}
