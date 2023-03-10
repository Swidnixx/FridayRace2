using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBase : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public Transform wheelModel;
    public bool frontWheel;

    private void Update()
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose( out pos, out rot );

        wheelModel.position = pos;
        wheelModel.rotation = rot;
    }
}
