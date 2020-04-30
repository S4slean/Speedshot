using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private TeamEnum teamGoal;
    private SpriteRenderer sprite;
    private bool isTriggerable = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        switch (teamGoal)
        {
            case TeamEnum.TEAM1:
                sprite.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.5f);
                break;
            case TeamEnum.TEAM2:
                sprite.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
                break;
            default:
                sprite.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Player" && isTriggerable)
        {
            if (col.GetComponent<Character>().hasTheBall && col.GetComponent<Character>().team != teamGoal)
            {
                StartCoroutine(TriggerGoal());
                isTriggerable = false;
            }
        }
    }

    IEnumerator TriggerGoal()
    {
        Debug.Log("GOAAAAAL!!!!");
        GameManager.instance.ball.GetComponent<Rigidbody2D>().velocity *= 0.1f;
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.ball.Grabber.hasTheBall = false;
        GameManager.instance.ball.SetAsNotGrabbed(Vector2.zero);
        GameManager.instance.ball.gameObject.SetActive(false);
        isTriggerable = true;

        switch (teamGoal)
        {
            case TeamEnum.TEAM1:
                ScoreManager.instance.Goal(TeamEnum.TEAM2);
                break;
            case TeamEnum.TEAM2:
                ScoreManager.instance.Goal(TeamEnum.TEAM1);
                break;
        }

        //Display goal anim
        //Time.timeScale = 0.5f;
        UIManager.instance.StartGoalAnim();
    }

}
