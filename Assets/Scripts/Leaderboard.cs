using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI[] texts;

    private void Start()
    {
        foreach( var t in texts)
        {
            t.text = "";
        }
    }

    private void Update()
    {
        List<string> players = GetPlayersList();

        for(int i=0; i<players.Count; i++)
        {
            texts[i].text = players[i];
        }
    }

    static List<PlayerScore> playersScores = new List<PlayerScore>();
    public static int Register(string playerName)
    {
        playersScores.Add(new PlayerScore(playerName) );
        return playersScores.Count - 1;
    }

    public static void SetPosition(int leaderboardId, int lap, int checkPoint)
    {
        playersScores[leaderboardId].SetScore(lap, checkPoint);
    }

    private List<string> GetPlayersList()
    {
        return playersScores.OrderByDescending(ps => ps.score).Select( ps => ps.name).ToList();
    }

    class PlayerScore
    {
        public string name { get; private set; }
        public int score { get; private set; }

        public PlayerScore(string playerName)
        {
            name = playerName;
            score = 0;
        }

        public void SetScore(int lap, int checkpoint)
        {
            score = lap * 1000 + checkpoint;
        }
    }

}
