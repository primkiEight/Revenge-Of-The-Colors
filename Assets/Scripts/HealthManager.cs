using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour {

    //Health
    public float MaxHP = 1.0f;
    public float CurrentHP;
    //public float RespawnHealthPoints = 1.0f;

    //Life
    public bool IsAlive = true;
    public int NumberOfLives = 1;
    private int _currentLives;

    //Invincibility
    public bool IsInvincible = false;
    private string InvincibilityAppearance = "InvincibilityOFF";

    //Respawn
    public Vector3 RespawnPosition;
    public Quaternion RespawnRotation;
    public bool IsRespawning = false;
    public float InvincibleDurationOnRespawn = 1.0f;
    public float WaitToRespawn;

    //DeathEffect
    public GameObject ExplosionPrefab;

    //DeathActions
    public OnDeathAction OnAllLivesGone = OnDeathAction.Nothing;

    public enum OnDeathAction
    {
        ReloadLevel,        
        GameOver,
        Nothing
    }

    private void Start()
    {
        RespawnPosition = transform.position;
        RespawnRotation = transform.rotation;

        CurrentHP = MaxHP;
        _currentLives = NumberOfLives;      
        
        if(transform.tag == "Player")
        {
            GameManager.GM.TextLives.text = "Lives: " + _currentLives.ToString();            
        }
            
    }

    private void Update()
    {
        if (CurrentHP <= 0.0f && !IsRespawning)
        {
            GameManager.GM.DeathSound.Play();

            _currentLives--; //pisanje ovoga ovdje, ne radi nam ispravno deathzone koji poziva respawn pa nam ne umanjuje broj života
            if (transform.tag == "Player")
                GameManager.GM.TextLives.text = "Lives: " + _currentLives.ToString();

            if (ExplosionPrefab)
            {
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity); //identity = nema rotacije
            }

            if (_currentLives > 0) // && !IsRespawning)
            {
                if(transform.tag != "Player")
                {
                    //Sada ne treba, enemy ima samo 0 života
                } else if (transform.tag == "Player")
                {
                    GameManager.GM.StartCoroutine("RespawnCo");
                }
                
            } else
            {
                switch (OnAllLivesGone)
                {
                    case OnDeathAction.ReloadLevel:
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        break;
                        
                    case OnDeathAction.GameOver:
                        GameManager.GM.GameOver();
                        break;

                    case OnDeathAction.Nothing:
                        break;

                    default:
                        break;
                }

                IsAlive = false;

                Destroy(gameObject);
            }
            
        }        
    }
    
    public void ApplyDamage(float amount)
    {
        if (!IsInvincible)
        {
            CurrentHP -= amount;
            if(transform.tag == "Damagable")
                transform.Translate(Vector3.back);
            // rasprši krv :D            
        }
    }
    /*
    public IEnumerator RespawnCo()
    {
        IsRespawning = true;
        IsAlive = false;
        //GameManager.GM.SetActiveGameObject(gameObject, false);

        yield return new WaitForSeconds(WaitToRespawn);

        transform.position = _respawnPosition;
        transform.rotation = _respawnRotation;
        //CurrentHP = RespawnHealthPoints;
        CurrentHP = MaxHP;

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (transform.tag == "Player")
        {
            StartCoroutine("InvincibilityCo", (InvincibleDurationOnRespawn));
            PlayerController thePlayer = GetComponent<PlayerController>();
            thePlayer.ResetPlayer();
        }
        
        IsRespawning = false;
        IsAlive = true;
        //GameManager.GM.SetActiveGameObject(gameObject, true);
    }*/
    	
    public void SetRespawnPosition(Vector3 newRespawnPosition)
    {
        RespawnPosition = newRespawnPosition;        
    }

    public void HealUp(float HealValue)
    {
        if(CurrentHP < MaxHP)
            CurrentHP += HealValue;
        if(CurrentHP >= MaxHP)
            CurrentHP = MaxHP;
    }

    public void LifeUp(int LifeValue)
    {
        _currentLives += LifeValue;
        if(GetComponent<PlayerController>())
            GameManager.GM.TextLives.text = "Lives: " + _currentLives.ToString();
    }

    public IEnumerator InvincibilityCo(float InvincibilityDuration)
    {
        IsInvincible = true;
        GameManager.GM.InvincibilityTicTacSound.Play();
        PlayerController thePlayer = GetComponent<PlayerController>();
        thePlayer.ChangeAppearance("InvincibilityON");
        //prikaži da je invincible
        //GameManager.GM.IsBoosted = false;
        yield return new WaitForSeconds(InvincibilityDuration); //Vector.x duration
        thePlayer.ChangeAppearance("InvincibilityOFF");
        //prikaži da više nije invincible
        //GameManager.GM.IsBoosted = false;
        IsInvincible = false;
        GameManager.GM.InvincibilityTicTacSound.Stop();
    }
}
