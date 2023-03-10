using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    public WheelBase[] wheels;
    public float motorTorque = 500;
    public float maxSteerAngle = 30;

    private void Update()
    {
        float steer = Input.GetAxis("Horizontal");
        float accel = Input.GetAxis("Vertical");

        foreach( var w in wheels )
        {
            w.wheelCollider.motorTorque = accel * motorTorque;
            if(w.frontWheel)
            {
                w.wheelCollider.steerAngle = steer * maxSteerAngle;
            }
        }
    }
}
