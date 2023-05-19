using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform[] spawnPos;

    public GameObject startRacePanel;
    public GameObject waitingTextPanel;

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
        var car = PhotonNetwork.Instantiate(
            carPrefab.name,
            spawnPos[playerIndex].position, spawnPos[playerIndex].rotation,
            0, instanceData
        );

        //Local Player Setup
        car.GetComponent<CarAppearance>().SetPlayerNumber(playerIndex); // do zastanowienia
        car.GetComponent<PlayerController>().enabled = true;
        GameObject.FindObjectOfType<CameraController>().SetCameraToCar(car.transform.GetChild(0));
        var backCam = car.transform.GetChild(0).Find("BackCamera");
        backCam.gameObject.SetActive(true);

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
}
