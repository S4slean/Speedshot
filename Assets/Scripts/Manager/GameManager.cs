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
        SetPlayersMovable(false);
        SetBallMovable(false);

        //Spawn Players
        SpawnTeams();

        //Spawn Ball
        SpawnBall();

        StartGame();
    }

    public void StartGame()
    {
        //Start Countdown
        UIManager.instance.StartCountdown();
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
        p.gameObject.SetActive(true);
        p.transform.position = spawnPoint.position;
    }

    public void SpawnBall()
    {
        ball.gameObject.SetActive(true);
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
            if(!movable)
            {
                p.GetComponent<Rigidbody2D>().gravityScale = 0f;
                p.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                p.enabled = false;
            }
        }
    }

    public void SetBallMovable(bool movable)
    {
        ball.IsSubjectToGravity = movable;
        if(!movable)
        {
            ball.Restart();
        }
    }

    public void RespawnAfterGoal()
    {
        //Freeze actors
        SetPlayersMovable(false);
        SetBallMovable(false);

        //Respawn players
        SpawnTeams();
        //Respawn ball
        SpawnBall();

        //Start CountDown
        UIManager.instance.StartCountdown();
    }
}
