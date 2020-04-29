﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Team teamGoal;
    private SpriteRenderer sprite;
    private bool isTriggerable = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        switch (teamGoal)
        {
            case Team.Blue:
                sprite.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.5f);
                break;
            case Team.Red:
                sprite.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
                break;
            default:
                sprite.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Ball" && isTriggerable)
        {
            StartCoroutine(TriggerGoal());
            isTriggerable = false;
        }
    }

    IEnumerator TriggerGoal()
    {
        switch (teamGoal)
        {
            case Team.Blue:
                ScoreManager.Instance.Goal(Team.Red);
                break;
            case Team.Red:
                ScoreManager.Instance.Goal(Team.Blue);
                break;
        }

        //Display GOAL! Screen
        Debug.Log("GOAAAAAL!!!!");
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1);

        //Freeze actors
        GameManager.instance.SetPlayersMovable(false);
        GameManager.instance.SetBallMovable(false);

        //Respawn players
        GameManager.instance.SpawnTeams();
        //Respawn ball
        GameManager.instance.SpawnBall();

        //Start CountDown
        UIManager.instance.StartCountdown(3);

        isTriggerable = true;
    }

}
