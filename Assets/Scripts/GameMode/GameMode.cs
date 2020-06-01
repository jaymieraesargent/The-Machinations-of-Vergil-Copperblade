using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    //set up
    public int numberOfTeams = 2;
    public int numberOfPlayers;

    public List<Team> teams = new List<Team>();
    public List<Transform> spawnPoints;

    protected void Start()
    {
        Debug.Log("Setting up game");
        SetUpGame();
    }

    public void SetUpGame()
    {
        for(int teamID = 0; teamID < numberOfTeams; teamID++)
        {
            teams.Add(new Team());
        }
    }

    public void AddScore(int teamID, int score)
    {
        teams[teamID].score += score;
    }
}

[System.Serializable]
public class Team
{
    public int score;

}