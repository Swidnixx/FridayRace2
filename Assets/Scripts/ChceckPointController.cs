using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChceckPointController : MonoBehaviour
{
    public int lap = 0;
    public int chceckPoint;
    public int nextPoint;
    int pointCount;

    void Start()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        pointCount = checkpoints.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            int thisPoint = int.Parse(other.gameObject.name);
            if (thisPoint == nextPoint)
            {
                chceckPoint = thisPoint;
                if (chceckPoint == 0)
                {
                    lap++;
                    Debug.Log("Lap: " + lap);
                }

                nextPoint++;
                nextPoint = nextPoint % pointCount;
            }
        }
    }

}
