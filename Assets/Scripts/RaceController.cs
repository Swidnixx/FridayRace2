using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racePending = false;
    public static int totalLaps = 1;

    public int timer = 3;

    public ChceckPointController[] carsController;

    private void Start()
    {
        InvokeRepeating(nameof(CountDown), 3, 1);

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new ChceckPointController[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<ChceckPointController>();
        }
    }

    private void LateUpdate()
    {
        int fishedLap = 0;
        foreach (ChceckPointController controller in carsController)
        {
            if (controller.lap == totalLaps + 1)
            {
                fishedLap++;
            }

            if (fishedLap == carsController.Length && racePending)
            {
                Debug.Log("Koniec wyścigu !");
                racePending = false;
            }

        }
    }

    void CountDown()
    {
        if (timer > 0)
        {
            Debug.Log("Rozpoczęcie wyścigu za: " + timer);
            timer--;
        }
        else
        {
            Debug.Log("Start");
            racePending = true;
            CancelInvoke();
        }
    }
}
