using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    //set up
    //public int teamAmmount = 2;

    public List<Team> teams;
    public List<Transform> spawnPoints;
    public List<Text> scoreText;

    protected void Start()
    {
        Debug.Log("Setting up game");
        SetUpGame();
    }

    public void SetUpGame()
    {
        //for(int teamID = 0; teamID < teamAmmount; teamID++)
        //{
        //    teams.Add(new Team());
        //}
    }

    public void AddScore(int teamID, int score)
    {
        teams[teamID].score += score;
        scoreText[teamID].text = score.ToString();
    }
}

[System.Serializable]
public class Team
{
    public int score;
}

