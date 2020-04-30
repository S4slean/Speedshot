using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [HideInInspector] public int blueTeamScore, redTeamScore;

    [Header("Final Score")] public int FinalScore = 9;

    public TextMeshProUGUI blueTeamScoretxt, redTeamScoretxt;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void Goal(TeamEnum team)
    {
        TeamEnum winningTeam = TeamEnum.NONE;

        switch (team)
        {
            case TeamEnum.TEAM1:
                blueTeamScore++;
                if (blueTeamScore >= FinalScore)
                    winningTeam = TeamEnum.TEAM1;
                break;

            case TeamEnum.TEAM2:
                redTeamScore++;
                if (redTeamScore >= FinalScore)
                    winningTeam = TeamEnum.TEAM2;
                break;
        }

        UpdateScorePanel();
        if(winningTeam != TeamEnum.NONE)
        {
            TeamWinTheGame(winningTeam);
        }
    }

    public void UpdateScorePanel()
    {
        blueTeamScoretxt.text = blueTeamScore.ToString();
        redTeamScoretxt.text = redTeamScore.ToString();
    }

    public void TeamWinTheGame(TeamEnum winningTeam)
    {
        //End the Game
        //Display winning team screen

        switch (winningTeam)
        {
            case TeamEnum.TEAM1:
                Debug.Log("BLUE TEAM WIN THE GAME !");
                break;

            case TeamEnum.TEAM2:
                Debug.Log("RED TEAM WIN THE GAME !");
                break;
        }
    }
}
