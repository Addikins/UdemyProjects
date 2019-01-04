using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{
    int maxNumber = 1000;
    int minNumber = 1;
    int guess = 500;
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        maxNumber += 1;
        Debug.Log($@"Let's play a guessing game!
First off, think of a number, but don't tell me...
The highest number you can pick is {maxNumber}
and the lowest number you can pick is {minNumber}
I'll guess the number and you'll give me hints.
If my guess is higher than your number, push up.
If lower push down
If I guess correctly push Enter!
Alright let's play!

Is your number {guess}?");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            minNumber = guess;
            guess = (maxNumber + minNumber) / 2;
            Debug.Log($@"
Is your number {guess}? Higher or lower?");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            maxNumber = guess;
            guess = (maxNumber + minNumber) / 2;
            Debug.Log($@"
Is your number {guess}? Higher or lower?");
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($@"Success! Your number was {guess}!
Would you like to play again?");
        }
    }
}
