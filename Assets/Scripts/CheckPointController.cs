using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public Transform LastCheckpoint { get; private set; }
    public int Lap { get { return lap; } }

    int lap = 0;
    int checkPoint;
    int nextPoint;
    int pointCount;

    void Start()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        pointCount = checkpoints.Length;

        foreach(var c in checkpoints)
        {
            if(c.name == "0")
            {
                LastCheckpoint = c.transform;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            int thisPoint = int.Parse(other.gameObject.name);
            if (thisPoint == nextPoint)
            {
                LastCheckpoint = other.transform;

                checkPoint = thisPoint;
                if (checkPoint == 0)
                {
                    lap++;
                    Debug.Log("Lap: " + lap);
                }

                nextPoint++;
                nextPoint = nextPoint % pointCount;
            }
        }
    }

    int leaderboardId;
    bool registered;
    public void RegisterToLeaderboard(string playerName)
    {
        leaderboardId = Leaderboard.Register(playerName);
        registered = true;
    }

    private void Update()
    {
        if (!registered) return;
        Leaderboard.SetPosition(leaderboardId, lap, checkPoint);
    }

    public void Reset()
    {
        lap = 0;
        checkPoint = 0;
        nextPoint = 0;
    }
}
