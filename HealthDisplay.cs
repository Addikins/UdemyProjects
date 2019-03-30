using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    Player player;
    Player[] players;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        players = FindObjectsOfType<Player>();
        if (players[0].tag == tag)
        {
            player = players[0];
        }
        else
        {
            player = players[1];
        }
    }


    void Update()
    {
        healthText.text = player.GetHealth(tag).ToString();
    }
}
