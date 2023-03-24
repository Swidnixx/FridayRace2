using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public DrivingScript ds;

    private void Update()
    {
        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxisRaw("Jump");

        if (RaceController.racePending == false)
        {
            accel = 0;
        }

        ds.Drive(accel, steer, brake);
    }
}
