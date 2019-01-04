using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // config
    [Range(0.5f, 10f)] [SerializeField] float gameSpeed = 1.0f;
    [SerializeField] int pointsPerBlock = 125;
    [SerializeField] TextMeshProUGUI pointsText;

    // state variables
    [SerializeField] int totalPoints = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            DestroyGameStatus();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DestroyGameStatus()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void Start()
    {
        pointsText.text = totalPoints.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

    }

    public void SpeedUp()
    {
        gameSpeed += .01f;
    }

    public void AddPoints()
    {
        totalPoints += pointsPerBlock;
        pointsText.text = totalPoints.ToString();
    }
}
