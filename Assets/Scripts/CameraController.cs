using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    CinemachineTransposer transposer;

    public Vector3[] positions;
    int currentPos;

    private void Start()
    {
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            currentPos++;
            currentPos = currentPos % positions.Length;
            transposer.m_FollowOffset = positions[currentPos];
        }
    }

    public void SetCameraToCar(Transform car)
    {
        vcam.Follow = car;
        vcam.LookAt = car;
    }
}
