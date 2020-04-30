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

    public void SetupPlayerPortrait()
    {
        foreach(UI_PlayerPortrait portrait in allPlayerPortrait)
        {
            Debug.Log("Inactivate");
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
