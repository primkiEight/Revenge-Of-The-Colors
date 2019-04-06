using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour {

    public float WaitToLoad;
    public string LevelToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Time.timeScale = 0f;
            GameManager.GM.EndLevelSound.Play();
            Invoke("LoadLevel", WaitToLoad);
        }

    }
    
    void LoadLevel()
    {
        SceneManager.LoadScene(LevelToLoad);
    }


}
