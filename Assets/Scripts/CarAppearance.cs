using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarAppearance : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI nameText;
    public MeshRenderer carRenderer;

    string playerName = "Player";
    Color playerColor = Color.white;
    int playerNumber;

    private void Start()
    {
        if (photonView.IsMine)
        {
            playerName = PlayerPrefs.GetString("Nickname");
            playerColor = new Color(PlayerPrefs.GetFloat("R"), PlayerPrefs.GetFloat("G"), PlayerPrefs.GetFloat("B")); 
        }
        else
        {
            object[] data = photonView.InstantiationData;
            playerName = (string)data[0];
            playerColor = new Color( (float)data[1], (float)data[2], (float)data[3] );
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
