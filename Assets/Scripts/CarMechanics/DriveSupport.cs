using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSupport : MonoBehaviour
{
    float rolloverTime;
    float stuckTime;
    Rigidbody carRb;
    CheckPointController ckpController;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
        ckpController = GetComponent<CheckPointController>();
    }

    private void Update()
    {
        AntiRollover();
        AntiStuck();
    }
    void AntiStuck()
    {
        if(carRb.velocity.magnitude < 0.5f && RaceController.racePending)
        {
            stuckTime += Time.deltaTime;
        }
        else
        {
            stuckTime = 0;
        }

        if(stuckTime > 3.5f)
        {
            Vector3 spawnOffset = transform.position - ckpController.LastCheckpoint.position;
            spawnOffset.y = 0;
            if ( spawnOffset.magnitude > 1)
            {
                transform.position = ckpController.LastCheckpoint.position;
                transform.rotation = ckpController.LastCheckpoint.rotation;
                carRb.velocity = Vector3.zero; 
            }
            stuckTime = 0;
        }
    }

    void AntiRollover()
    {
        if (transform.up.y < 0.15)
        {
            rolloverTime += Time.deltaTime;
        }
        else
        {
            rolloverTime = 0;
        }

        if (rolloverTime > 2.5f)
        {
            transform.position += Vector3.up;
            transform.rotation = Quaternion.LookRotation(transform.forward);
        }
    }
}
