using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action<bool> PauseGameEvent;

    [Header("Spawn Points")]
    public Transform redTeamSpawnPointsContainer;
    public Transform blueTeamSpawnPointsContainer;
    public Transform ballSpawnPoint;

    [Header("Teams")]
    public List<Character> blueTeam;
    public List<Character> redTeam;

    [Header("Ball")]
    public Ball ball;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        
    }

    private void SetupGame()
    {
        //Spawn Players
        SpawnTeams();

        //Spawn Ball
        SpawnBall();

        //Start Countdown
    }

    public void StartGame()
    {

    }

    public void PauseGame(bool isPaused)
    {
        PauseGameEvent?.Invoke(isPaused);
        // Afficher Menu
    }

    public void LeaveGame()
    {

    }

    public void SpawnTeams()
    {
        int spawnIndex = 0;
        foreach (Character p in blueTeam)
        {
            SpawnPlayer(p, blueTeamSpawnPointsContainer.GetChild(spawnIndex).transform);
            spawnIndex++;
        }

        spawnIndex = 0;

        foreach (Character p in redTeam)
        {
            SpawnPlayer(p, redTeamSpawnPointsContainer.GetChild(spawnIndex).transform);
            spawnIndex++;
        }
    }

    public void SpawnPlayer(Character p, Transform spawnPoint)
    {
        p.transform.position = spawnPoint.position;
    }

    public void SpawnBall()
    {
        ball.transform.position = ballSpawnPoint.position;
    }

    public void SetPlayersMovable(bool movable)
    {
        List<Character> players = new List<Character>();
        players.AddRange(blueTeam);
        players.AddRange(redTeam);

        foreach(Character p in players)
        {
            p.enabled = movable;
            if(movable)
                p.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 1f);
            else
                p.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void SetBallMovable(bool movable)
    {
        ball.IsSubjectToGravity = movable;
    }
}
