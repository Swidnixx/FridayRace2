using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotator : MonoBehaviour
{
    public float speed = 1;

    private void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }
}
