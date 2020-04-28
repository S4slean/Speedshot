using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Team teamGoal;
    private SpriteRenderer sprite;

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
        if(col.transform.tag == "Ball")
        {
            TriggerGoal();
        }
    }

    public void TriggerGoal()
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

        //Respawn players
        //Respawn ball
    }

}
