using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI networkText;
    byte playersPerRoom = 4;
    bool connecting;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        if (connecting) return;

        connecting = true;
        PhotonNetwork.NickName = PlayerPrefs.GetString("Nickname");
        PhotonNetwork.ConnectUsingSettings();
        networkText.text = "Connecting...\n";
    }

    public override void OnConnectedToMaster()
    {
        networkText.text = "Connected to Server.\n";
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Joining Random Room failed: " + message + "; " + returnCode;
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = playersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        networkText.text += "Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players";
        PhotonNetwork.LoadLevel("TestTrack");
    }
}
