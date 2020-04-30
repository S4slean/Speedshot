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
}
