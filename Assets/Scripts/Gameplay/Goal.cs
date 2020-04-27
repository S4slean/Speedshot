using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Team teamGoal;
    private SpriteRenderer sprite;

    void Start()
    {
        switch (teamGoal)
        {
            case Team.Blue:
                sprite.color = Color.blue;
                break;
            case Team.Red:
                sprite.color = Color.red;
                break;
            default:
                sprite.color = Color.white;
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
