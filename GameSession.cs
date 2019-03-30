using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int p1Score = 0;
    int p2Score = 0;
    int score = 0;
    int health = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType(GetType()).Length;

        if (numberOfGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore(string tag)
    {
        score = p1Score + p2Score;
        if (tag == "Player1")
        {
            return p1Score;
        }
        else if (tag == "Player2")
        {
            return p2Score;
        }
        else
        {
            return score;
        }
    }

    public void AddToScore(int scoreValue, string tag)
    {
        if (tag == "Player1")
        { p1Score += scoreValue; }
        else { p2Score += scoreValue; }
    }

    public void ResetGame()
    { Destroy(gameObject); }
}
