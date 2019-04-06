using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room02Trigger : MonoBehaviour {

    public GameObject Reward01Invincible;
    public GameObject Reward02Coin;
    public GameObject Reward03Boost;
    public GameObject Reward04HealUp;
    public GameObject Reward05BigCoin;
    public GameObject Reward06HealUp;
    public GameObject Reward07Invincible;
    public GameObject Reward08LifeUp;

    public TrapTrigger Trigger01;
    public GameObject EnemySpawner01;
    public TrapTrigger Trigger02;
    public GameObject EnemySpawner02;
    public ColorTrigger ColorTriggerBox;

    public GameObject WallLeft;
    public GameObject WallRight;

    private bool _triggerSpawner01Active = false;
    private bool _triggerSpawner02Active = false;
    
    void Start () {
        Reward01Invincible.SetActive(false);
        Reward02Coin.SetActive(false);
        Reward03Boost.SetActive(false);
        Reward04HealUp.SetActive(false);
        Reward05BigCoin.SetActive(false);
        Reward06HealUp.SetActive(false);
        Reward07Invincible.SetActive(false);
        Reward08LifeUp.SetActive(false);
        EnemySpawner01.SetActive(false);
        EnemySpawner02.SetActive(false);
        ColorTriggerBox.gameObject.SetActive(false);
    }
	
	void Update () {
        if (Trigger01.IsActive && !_triggerSpawner01Active)
        {
            if (ColorTriggerBox.isActiveAndEnabled == false);
                ColorTriggerBox.gameObject.SetActive(true);
            EnemySpawner01.SetActive(true);
            _triggerSpawner01Active = true;
        }
        if (Trigger02.IsActive && !_triggerSpawner02Active)
        {
            if (ColorTriggerBox.isActiveAndEnabled == false);
                ColorTriggerBox.gameObject.SetActive(true);
            EnemySpawner02.SetActive(true);
            _triggerSpawner02Active = true;
        }

        if(ColorTriggerBox.GetComponentInChildren<Light>().color == Color.grey)
        {
            if (Reward01Invincible)
                Reward01Invincible.SetActive(true);
        }

        if (ColorTriggerBox.GetComponentInChildren<Light>().color == Color.red)
        {
            if (Reward02Coin)
                Reward02Coin.SetActive(true);
        }

        if (ColorTriggerBox.GetComponentInChildren<Light>().color == Color.yellow)
        {
            if (Reward03Boost)
                Reward03Boost.SetActive(true);
        }

        if (ColorTriggerBox.GetComponentInChildren<Light>().color == Color.green)
        {
            if (Reward04HealUp)
                Reward04HealUp.SetActive(true);
        }

        if (ColorTriggerBox.GetComponentInChildren<Light>().color == Color.cyan)
        {
            if (Reward05BigCoin)
                Reward05BigCoin.SetActive(true);
        }

        if (ColorTriggerBox.GetComponentInChildren<Light>().color == Color.blue)
        {
            if (Reward06HealUp)
                Reward06HealUp.SetActive(true);
        }

        if (ColorTriggerBox.GetComponentInChildren<Light>().color == Color.magenta)
        {
            if (Reward07Invincible)
                Reward07Invincible.SetActive(true);
        }

        if (ColorTriggerBox.GetComponentInChildren<Light>().color == Color.white)
        {
            if(EnemySpawner01)
                EnemySpawner01.SetActive(false);
            if (EnemySpawner02)
                EnemySpawner02.SetActive(false);
            if (Reward08LifeUp)
                Reward08LifeUp.SetActive(true);

            Invoke("OpenWalls", 3f);
            return;
        }
    }

    void OpenWalls()
    {
        WallLeft.transform.Translate(Vector3.left);
        WallRight.transform.Translate(Vector3.right);        
    }
}
