using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Team
{
    None = -1,
    Blue = 0,
    Red = 1
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [HideInInspector] public int blueTeamScore, redTeamScore;

    [Header("Final Score")] public int FinalScore = 9;

    public TextMeshProUGUI blueTeamScoretxt, redTeamScoretxt;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void Goal(Team team)
    {
        Team winningTeam = Team.None;

        switch (team)
        {
            case Team.Blue:
                blueTeamScore++;
                if (blueTeamScore >= FinalScore)
                    winningTeam = Team.Blue;
                break;

            case Team.Red:
                redTeamScore++;
                if (redTeamScore >= FinalScore)
                    winningTeam = Team.Red;
                break;
        }

        UpdateScorePanel();
        if(winningTeam != Team.None)
        {
            TeamWinTheGame(winningTeam);
        }
    }

    public void UpdateScorePanel()
    {
        blueTeamScoretxt.text = blueTeamScore.ToString();
        redTeamScoretxt.text = redTeamScore.ToString();
    }

    public void TeamWinTheGame(Team winningTeam)
    {
        //End the Game
        //Display winning team screen

        switch (winningTeam)
        {
            case Team.Blue:
                Debug.Log("BLUE TEAM WIN THE GAME !");
                break;

            case Team.Red:
                Debug.Log("RED TEAM WIN THE GAME !");
                break;
        }
    }
}
