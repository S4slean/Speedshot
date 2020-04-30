using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Screens")]
    public GameObject startGameScreen;
    public GoalAnim blueGoalScreen, redGoalScreen;
    public Countdown countdown;
    public EndPanelAnim blueVictoryScreen, redVictoryScreen;

    [Header("Player portraits Ref")]
    public Sprite redBackgroundColor;
    public Sprite blueBackgroundColor;
    public List<Sprite> indexPlayerSprite;

    [Header("Player portraits Slots")]
    public List<Transform> bluePlayerPortraitSlots;
    public List<Transform> RedPlayerPortraitSlots;

    [Header("All Player Portrait")]
    public List<UI_PlayerPortrait> allPlayerPortrait;

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

    public void StartGoalAnim(TeamEnum team)
    {
        switch (team)
        {
            case TeamEnum.TEAM1:
                blueGoalScreen.StartGoalAnim();
                break;
            case TeamEnum.TEAM2:
                redGoalScreen.StartGoalAnim();
                break;
        }
    }

    public void DisplayVictoryScreen(TeamEnum winningTeam)
    {
        switch (winningTeam)
        {
            case TeamEnum.TEAM1:
                blueVictoryScreen.StartEndAnim();
                break;
            
            case TeamEnum.TEAM2:
                redVictoryScreen.StartEndAnim();
                break;
            
        }
    }

    public void SetupPlayerPortrait()
    {
        foreach(UI_PlayerPortrait portrait in allPlayerPortrait)
        {
            //Debug.Log("Inactivate");
            portrait.gameObject.SetActive(false);
        }

        int portraitIndex = 0;
        List<Character> players = new List<Character>();
        players.AddRange(GameManager.instance.blueTeam);
        players.AddRange(GameManager.instance.redTeam);

        foreach(Character p in players)
        {
            p.portrait = allPlayerPortrait[portraitIndex];
            allPlayerPortrait[portraitIndex].BuildPortrait(p.playerID, p.team);
            allPlayerPortrait[portraitIndex].gameObject.SetActive(true);
            portraitIndex++;
        }
    }

    public void UpdateBallHolderPortrait()
    {
        List<Character> players = new List<Character>();
        players.AddRange(GameManager.instance.blueTeam);
        players.AddRange(GameManager.instance.redTeam);

        foreach (Character p in players)
        {
            p.portrait.UpdateHoldingSprite(p.hasTheBall);
        }
    }
}
