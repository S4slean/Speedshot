using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnim : MonoBehaviour
{
    public void StartGoalAnim()
    {
        GetComponent<Animator>().SetTrigger("goal");
    }

    public void OnGoalAnimEnd()
    {
        GameManager.instance.RespawnAfterGoal();
    }

    public void PlaySound()
    {
        AudioManager2D.instance.PlaySound("Event_BUT", Camera.main.transform.position);
    }
}
