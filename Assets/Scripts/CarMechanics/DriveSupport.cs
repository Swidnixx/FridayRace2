using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSupport : MonoBehaviour
{
    float rolloverTime;

    public float transformY;
    private void Update()
    {
        transformY = transform.up.y;

        if( transform.up.y < 0.15 )
        {
            rolloverTime += Time.deltaTime;
        }
        else
        {
            rolloverTime = 0;
        }

        if( rolloverTime > 2.5f )
        {
            transform.position += Vector3.up;
            transform.rotation = Quaternion.LookRotation( transform.forward );
        }
    }
}
