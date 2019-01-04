using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    //parameters
    [SerializeField] public int blocksLeft;

    //cached reference
    SceneLoader sceneLoader;

    public void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        blocksLeft++;
    }

    // Update is called once per frame
    public void BlockDestroyed()
    {
        blocksLeft--;
        if (blocksLeft <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
