using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (gameObject.tag != "StartMenu")
        {
            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    public void ResetGame()
    { Destroy(gameObject); }
}