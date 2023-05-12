using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public int playerCount = 4;
    public Transform[] spawnPos;

    public GameObject startRacePanel;
    public GameObject waitingTextPanel;

    private void Start()
    {
        startRacePanel.SetActive(false);
        waitingTextPanel.SetActive(false);

        for(int i=0; i<playerCount; i++)
        {
            var car = Instantiate(carPrefab,
                spawnPos[i].position, spawnPos[i].rotation);
            Transform carBody = car.GetComponentInChildren<DrivingScript>().transform;

            car.GetComponent<CarAppearance>().SetPlayerNumber(i);

            if(i==0)
            {
                car.GetComponent<PlayerController>().enabled = true;
                GameObject.FindObjectOfType<CameraController>()
                    .SetCameraToCar(carBody);
            }
            else
            {
                var backCam = carBody.Find("BackCamera");
                backCam.gameObject.SetActive(false);
            }
        }
    }
}
