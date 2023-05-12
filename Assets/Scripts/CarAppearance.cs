using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarAppearance : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public MeshRenderer carRenderer;

    string playerName = "Player";
    Color playerColor = Color.white;
    int playerNumber;

    private void Start()
    {
        if (playerNumber == 0)
        {
            playerName = PlayerPrefs.GetString("Nickname");
            playerColor = new Color(PlayerPrefs.GetFloat("R"), PlayerPrefs.GetFloat("G"), PlayerPrefs.GetFloat("B")); 
        }
        else
        {
            playerName = "Random" + playerNumber;
            playerColor = Color.black;
        }

        nameText.text = playerName;
        nameText.color = playerColor;

        carRenderer.material.color = playerColor;
    }

    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
    }
}
