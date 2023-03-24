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

    public void Drive(float accel, float steer, float brake)
    {
        EngineSound(accel);
        speed = rb.velocity.magnitude * 3.6f;

        if ( speed > maxSpeed )
        {
            accel = 0;
        }

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

    public AudioSource engineAudioSource;
    public float minPitch = 0.55f;
    public float maxPitch = 1.1f;
    public float gearChangeTime = 5;
    public int maxGear = 4;

    float accelTime;
    int currentGear = 1;
    void EngineSound(float accel)
    {
        if (speed < 15)
            currentGear = 1;

        accel = Mathf.Abs(accel);

        if( accel > 0 )
        {
            accelTime += Time.deltaTime;
            accelTime = Mathf.Min( accelTime, gearChangeTime );
        }
        else
        {
            accelTime -= Time.deltaTime;
            accelTime = Mathf.Max( accelTime, 0 );
        }

        float normalizedAccelTime = accelTime / gearChangeTime;
        engineAudioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedAccelTime);

        if( accelTime == gearChangeTime && currentGear < maxGear )
        {
            currentGear++;
            accelTime = 0 + 0.2f * gearChangeTime;
        }

        if( accelTime == 0 && currentGear > 1 )
        {
            currentGear--;
            accelTime = gearChangeTime - 0.2f * gearChangeTime;
        }
    }
}
