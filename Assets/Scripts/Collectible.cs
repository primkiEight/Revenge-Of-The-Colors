using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public ColectibleItem Item = ColectibleItem.Nothing;

    public enum ColectibleItem
    {
        Jump,
        Coin,
        HealUp,
        LifeUp,
        Booster,
        Invincibility,
        Treasure,
        Nothing
    }
    
    public int CoinValue = 1;
    public float HealUpValue = 1.0f;
    public int LifeUpValue = 1;
    [Header("X SpeedModifier, Y Duration")]
    public Vector2 Booster = new Vector2(1.0f, 1.0f);
    public float InvincibilityDuration = 1.0f;
    public int TreasureValue = 1;
    
    //Collectible
    private void OnTriggerEnter(Collider other)
    {
        //PlayerCanJump
        if (Item == ColectibleItem.Jump && other.gameObject.tag == "Player")
        {
            PlayerController thePlayer = other.GetComponent<PlayerController>();
            if (thePlayer)
                thePlayer.CanJump(); //vector.x is speed, vector.y is duration
            GameManager.GM.JumpSound.Play();
            Destroy(gameObject);
        }

        //Coin
        if (Item == ColectibleItem.Coin && other.gameObject.tag == "Player")
        {
            GameManager.GM.Collect(CoinValue);
            GameManager.GM.CoinSound.Play();
            Destroy(gameObject);
        }

        //HealUp
        if (Item == ColectibleItem.HealUp && other.gameObject.tag == "Player")
        {
            HealthManager theHealthManager = other.GetComponent<HealthManager>();
            if (theHealthManager)
                theHealthManager.HealUp(HealUpValue);
            GameManager.GM.HealUpSound.Play();
            Destroy(gameObject);
        }

        //LifeUp
        if (Item == ColectibleItem.LifeUp && other.gameObject.tag == "Player")
        {
            HealthManager theHealthManager = other.GetComponent<HealthManager>();
            if (theHealthManager)
                theHealthManager.LifeUp(LifeUpValue);
            GameManager.GM.LifeUpSound.Play();
            Destroy(gameObject);
        }

        //Booster
        if (Item == ColectibleItem.Booster && other.gameObject.tag == "Player")
        {
            PlayerController thePlayer = other.GetComponent<PlayerController>();
            if (thePlayer)
                thePlayer.StartCoroutine("BoostPlayerCo",(Booster)); //vector.x is speed, vector.y is duration
            GameManager.GM.BoostSound.Play();
            Destroy(gameObject);
        }

        //Invincible
        if (Item == ColectibleItem.Invincibility && other.gameObject.tag == "Player")
        {
            HealthManager theHealthManager = other.GetComponent<HealthManager>();
            if (theHealthManager)
                theHealthManager.StartCoroutine("InvincibilityCo", (InvincibilityDuration));
            GameManager.GM.InvincibilitySound.Play();
            Destroy(gameObject);
        }
    }

    //TreasureGoblin
    private void OnCollisionEnter(Collision other) //napravili zbog TreasureGoblina
    {
        if (Item == ColectibleItem.Treasure && other.gameObject.tag == "Player")
        {
            GameManager.GM.Collect(TreasureValue);
            Destroy(gameObject);
        }
    }
}
