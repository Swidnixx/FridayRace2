using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviourPunCallbacks
{
    public static event Action RaceStarted;

    public static bool racePending = false;
    public static int totalLaps = 1;

    public int timer = 3;

    public CheckPointController[] carsController;

    public TextMeshProUGUI startText;
    public GameObject startPanel;
    public GameObject finishPanel;

    private void Start()
    {
        startPanel.SetActive(false);
        finishPanel.SetActive(false);
    }

    public void StartRaceButton()
    {
        photonView.RPC(nameof(StartRace), RpcTarget.All, null);
    }

    [PunRPC] //Zdalne uruchomienie procedury
    private void StartRace()
    {
        RaceStarted?.Invoke();

        startPanel.SetActive(true);

        InvokeRepeating(nameof(CountDown), 3, 1);
        startText.text = timer.ToString();

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }
    void CountDown()
    {
        if (timer > 1)
        {
            timer--;
            startText.text = timer.ToString();
        }
        else if( timer > 0)
        {
            startText.text = "START!";
            racePending = true;
            timer--;
        }
        else
        {
            CancelInvoke();
            startPanel.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        int finishers = 0;
        foreach (CheckPointController controller in carsController)
        {
            if (controller.Lap == totalLaps + 1)
            {
                finishers++;
            }
        }

        if (finishers == carsController.Length && racePending)
        {
            finishPanel.SetActive(true);
            racePending = false;
        }
    }

    public void RestartRace()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}
