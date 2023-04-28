using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknameCanvas : MonoBehaviour
{
    Transform mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 dir = transform.position - mainCamera.position;
        transform.forward = dir;
    }
}
