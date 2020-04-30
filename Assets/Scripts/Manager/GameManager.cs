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

    private TeamEnum winningTeam = TeamEnum.NONE;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        //Test
        SetupGame();
    }

    public void SetWinningTeam(TeamEnum team)
    {
        winningTeam = team;
    }

    private void SetupGame()
    {
        winningTeam = TeamEnum.NONE;

        //Setup player portrait
        UIManager.instance.SetupPlayerPortrait();

        SetPlayersMovable(false);
        SetBallMovable(false);

        //Spawn Players
        SpawnTeams();

		//Spawn Ball
		ball = GameObject.FindObjectOfType<Ball>();
        SpawnBall();

        StartGame();
    }

    public void StartGame()
    {
        //Start Countdown
        UIManager.instance.StartCountdown();

        AudioManager2D.instance?.PlaySound("Event_Cheer", Camera.main.transform.position);
    }

    public void PauseGame(bool isPaused)
    {
        PauseGameEvent?.Invoke(isPaused);
        // Afficher Menu
    }

    public void EndGame()
    {
        //Display winning Screen
        UIManager.instance.DisplayVictoryScreen(winningTeam);

        //Wait for input to leave the game;
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

        AudioManager2D.instance?.PlaySound("Event_BalleRemiseJeu", ball.transform.position);
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
        if (winningTeam == TeamEnum.NONE)
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
        else
        {
            EndGame();
        }
    }
}
