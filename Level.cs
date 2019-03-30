using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayTime = 3f;

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayTime);
        if (SceneManager.GetActiveScene().name == "Multiplayer")
        {
            SceneManager.LoadScene("Game Over Multiplayer");
        }
        else
        {
            SceneManager.LoadScene("Game Over Singleplayer");
        }
    }

    public void LoadSinglePlayer()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Single Player");
    }

    public void LoadMultiplayer()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Multiplayer");
    }

    public void LoadStartMenu()
    {
        FindObjectOfType<MusicPlayer>().ResetGame();
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
