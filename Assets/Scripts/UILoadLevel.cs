using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoadLevel : MonoBehaviour {

    public int mainMenuIndex;

    public void ResumeGame()
    {        
        GameManager.GM.GameplayCanvas.enabled = true;
        GameManager.GM.GameOverCanvas.enabled = false;
        Time.timeScale = 1f;
        for (int i = 0; i < GameManager.GM.AllAudioSources.Length; i++)
        {
            GameManager.GM.AllAudioSources[i].UnPause();
        }
        GameManager.GM.MainMenuAudioSource.Stop();
        if(GameManager.GM.IsBoosted == false)
            GameManager.GM.BackgroundAudioSource.pitch = 1f;
    }

    public void ReloadLevel(int levelIndex) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu(int levelIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuIndex);
    }
}
