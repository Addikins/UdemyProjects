using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;

    //state
    Vector2 paddleToBallVector;
    bool hasStarted;

    // Cached component references
    AudioSource myAudioSource;

    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        hasStarted = false;
        xPush = UnityEngine.Random.Range(-4f, 4f);
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hasStarted)
        {
            LaunchOnClick();
            StickBallToPaddle();
        }
        ResetCurrentLevel();
    }

    private static void ResetCurrentLevel()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var sceneIndex = SceneManager.GetActiveScene();
            SceneManager.LoadScene(sceneIndex.buildIndex);
            FindObjectOfType<GameSession>().DestroyGameStatus();
        }
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void StickBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
        }
    }
}
