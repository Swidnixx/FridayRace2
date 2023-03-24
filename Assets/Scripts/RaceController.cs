using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racePending = false;
    public static int totalLaps = 1;

    public int timer = 3;

    private void Start()
    {
        InvokeRepeating(nameof(CountDown), 3, 1);
    }

    void CountDown()
    {
        if(timer > 0)
        {
            Debug.Log("Rozpoczêcie wyœcigu za: " + timer);
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
