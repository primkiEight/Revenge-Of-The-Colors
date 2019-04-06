using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //design patterns - pogledati
    //singleton - pogledati, ovo je design pattern koji rješava da imamo samo jedan objekt jedne klase, kao što je GameManager
    public static GameManager GM; //definiramo statičnu, svima javno dostupnu i svima identičnu varijablu (ovo sada i dalje nije singleton, možemo napraviti više objekata s ovom klasom)

    //MainCamera
    public CameraController mainCamera;
    
    //Gameplay Canvas
    public Canvas GameplayCanvas;
    public Text TextScore;
    public Text TextTimer;
    public Text TextBoost;
    public Text TextLives;

    //GameOver Canvas
    public Canvas GameOverCanvas;
    public Text FinalScore;

    //Score
    public int CurrentScoreValue = 0;    

    //Timer
    private float _stopwatch = 0.0f;

    //Boost
    public bool IsBoosted;

    //CheckPoints
    private GameObject _activeCheckPoint = null;
    private CheckPoint[] _checkPoints;
    private Light _light;

    //OpenRooms
    public bool Room01 = false;

    //PlayerController
    public PlayerController thePlayer;
    private HealthManager _playerHM;

    //Audio
    public AudioSource MainMenuAudioSource;
    public AudioSource BackgroundAudioSource;
    public AudioSource GameOverSound;
    public AudioSource JumpSound;
    public AudioSource CoinSound;
    public AudioSource CoinBigSound;
    public AudioSource LifeUpSound;
    public AudioSource HealUpSound;
    public AudioSource CheckPointSound;
    public AudioSource RespawnSound;
    public AudioSource InvincibilitySound;
    public AudioSource InvincibilityTicTacSound;
    public AudioSource BoostSound;
    public AudioSource DeathSound;
    public AudioSource EndLevelSound;
    public AudioSource[] AllAudioSources;

    private void Awake()
    {
        if (GM == null)
            GM = this; //referencira se na samog sebe; vrijednost javne statične varijable sam "ja"

        if (thePlayer == null)
        {
            thePlayer = FindObjectOfType<PlayerController>();
            //_playerHM = _playerController.GetComponent<HealthManager>(); //pretpostavka je da player ima HealthManager skriptu na sebi (ovdje nismo napravili provjeru)
        }

        mainCamera = FindObjectOfType<CameraController>();

        _checkPoints = FindObjectsOfType<CheckPoint>();

        AllAudioSources = FindObjectsOfType<AudioSource>();

    }

    void Start () {
        GameplayCanvas.enabled = true;
        GameOverCanvas.enabled = false;

        TextScore.text = "Score: " + CurrentScoreValue;        
        TextBoost.enabled = false;       
    }
	
	void Update () {
        _stopwatch += Time.deltaTime;
        TextTimer.text = "Timer: " + _stopwatch.ToString("F1"); //formatira u broj s jednim decimalnim mjestom

        if (TextBoost) //ako postoji, zna bit nevidljiv ako je objekt mrtav pa se pojavljuje error
        {
            if (IsBoosted)
                TextBoost.enabled = true;
            else
                TextBoost.enabled = false;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;

        FinalScore.text = "Score: " + CurrentScoreValue;
        GameplayCanvas.enabled = false;
        GameOverCanvas.enabled = true;

        for (int i = 0; i < AllAudioSources.Length; i++)
        {
            AllAudioSources[i].Pause();
        }

        MainMenuAudioSource.Play();
    }

    public void SetActiveCheckPoint(GameObject ActiveCheckPoint)
    {
        for (int i = 0; i < _checkPoints.Length; i++)
        {
            _light = _checkPoints[i].GetComponentInChildren<Light>();

            if (_light.isActiveAndEnabled)
            {
                _light.enabled = false;
            }
        }

        _activeCheckPoint = ActiveCheckPoint;
        ActiveCheckPoint.GetComponentInChildren<Light>().enabled = true;
        CheckPointSound.Play();
    }

    public void Collect(int amount)
    {
        CurrentScoreValue += amount;
        TextScore.text = "Score: " + CurrentScoreValue.ToString();
    }

    public IEnumerator RespawnCo()
    {
        HealthManager PlayerHM = thePlayer.GetComponent<HealthManager>();
        PlayerHM.IsRespawning = true;
        thePlayer.GetComponent<HealthManager>().IsAlive = false;

        thePlayer.gameObject.SetActive(false);
        GameManager.GM.RespawnSound.Play();
        yield return new WaitForSeconds(PlayerHM.WaitToRespawn);
        thePlayer.gameObject.SetActive(true);

        thePlayer.ResetPlayer();
        thePlayer.transform.position = PlayerHM.RespawnPosition;
        thePlayer.transform.rotation = PlayerHM.RespawnRotation;

        //CurrentHP = RespawnHealthPoints;
        PlayerHM.CurrentHP = PlayerHM.MaxHP;
        PlayerHM.StartCoroutine("InvincibilityCo", (PlayerHM.InvincibleDurationOnRespawn));

        if (BackgroundAudioSource.pitch != 1f)
            BackgroundAudioSource.pitch = 1f;

        PlayerHM.IsRespawning = false;
        PlayerHM.IsAlive = true;        
    }

    //za potrebe praćenja i otvaranja sljedećih nivoa
    public void OpenRoom(string RoomNumber)
    {
        if (RoomNumber == "Room01")
            Room01 = true;
    }
}
