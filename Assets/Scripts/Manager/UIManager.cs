using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Screens")]
    public GameObject startGameScreen;
    public GoalAnim goalScreen;
    public Countdown countdown;
    public GameObject blueVictoryScreen, redVictoryScreen;

    [Header("Player portraits")]
    public GameObject bluePlayer1;
    public GameObject bluePlayer2;
    public GameObject redPlayer1;
    public GameObject redPlayer2;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void StartCountdown()
    {
        countdown.StartCountdown();
    }

    public void StartGoalAnim()
    {
        goalScreen.StartGoalAnim();
    }

    public void DisplayVictoryScreen(TeamEnum winningTeam)
    {
        switch (winningTeam)
        {
            case TeamEnum.TEAM1:
                blueVictoryScreen.SetActive(true);
                break;
            
            case TeamEnum.TEAM2:
                redVictoryScreen.SetActive(true);
                break;
            
        }
    }

    public void DisplayBallHolder()
    {
        List<Character> players = new List<Character>();
        players.AddRange(GameManager.instance.blueTeam);
        players.AddRange(GameManager.instance.redTeam);

        foreach(Character p in players)
        {
            if (p.hasTheBall)
            {

            }
            else
            {

            }
        }
    }
}
