using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberWizard : MonoBehaviour
{
    [SerializeField] int maxNumber = 1000;
    [SerializeField] int minNumber = 1;
    [SerializeField] TextMeshProUGUI guessText;
    int guess;
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        NextGuess();
    }

    public void OnPressHigher()
    {
        minNumber = guess + 1;
        NextGuess();
    }
    public void OnPressLower()
    {
        maxNumber = guess - 1;
        NextGuess();
    }
    void NextGuess()
    {
        guess = Random.Range(minNumber, maxNumber + 1);
        guessText.text = guess.ToString();
    }
}
