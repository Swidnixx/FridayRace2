using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform[] spawnPos;

    public GameObject startRacePanel;
    public GameObject waitingTextPanel;

    GameObject localPlayer;
    int playerIndex;

    private void Awake()
    {
        RaceController.RaceStarted += OnRaceStarted;
    }
    private void OnRaceStarted()
    {
        startRacePanel.SetActive(false);
        waitingTextPanel.SetActive(false);
    }

    private void Start()
    {
        playerIndex = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        startRacePanel.SetActive(false);
        waitingTextPanel.SetActive(false);

        //Network Player Instantiaton
        object[] instanceData = { 
            PlayerPrefs.GetString("Nickname"),
            PlayerPrefs.GetFloat("R"),  PlayerPrefs.GetFloat("G"), PlayerPrefs.GetFloat("B")
        };
        var car = localPlayer = PhotonNetwork.Instantiate(
            carPrefab.name,
            spawnPos[playerIndex].position, spawnPos[playerIndex].rotation,
            0, instanceData
        );

        //Local Player Setup
        car.GetComponent<PlayerController>().ActivateLocally();
        GameObject.FindObjectOfType<CameraController>().SetCameraToCar(car.transform.GetChild(0));

        //Master Client Setup
        if(PhotonNetwork.IsMasterClient)
        {
            startRacePanel.SetActive(true);
        }
        else
        {
            waitingTextPanel.SetActive(true);
        }
    }

    public void RespawnLocalPlayer()
    {
        Transform car = localPlayer.transform.GetChild(0);
        car.GetComponent<DrivingScript>().StopWheels();
        var rb = car.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        car.position = spawnPos[playerIndex].position;
        car.rotation = spawnPos[playerIndex].rotation;

        if (PhotonNetwork.IsMasterClient)
        {
            startRacePanel.SetActive(true);
        }
        else
        {
            waitingTextPanel.SetActive(true);
        }
    }
}
