using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    public WheelBase[] wheels;
    public float motorTorque = 500;
    public float maxSteerAngle = 30;
    public float brakeTorque = 2000;

    Rigidbody rb;
    public Transform centerOfMass;

    public float speed;
    public float maxSpeed = 50;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void Update()
    {
        speed = rb.velocity.magnitude * 3.6f;

        float steer = Input.GetAxis("Horizontal");
        float accel = Input.GetAxis("Vertical");
        if( speed > maxSpeed )
        {
            accel = 0;
        }
        float brake = Input.GetAxisRaw("Jump");

        foreach( var w in wheels )
        {
            w.wheelCollider.motorTorque = accel * motorTorque;
            w.wheelCollider.brakeTorque = brake * brakeTorque;

            if(w.frontWheel)
            {
                w.wheelCollider.steerAngle = steer * maxSteerAngle;
            }
        }
    }
}
