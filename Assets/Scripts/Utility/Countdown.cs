using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public void StartCountdown()
    {
        GetComponent<Animator>().SetTrigger("counting");
        //GetComponent<Animator>().ResetTrigger("counting");
    }

    public void SetTimeScale(int x)
    {
        Time.timeScale = x;
        GameManager.instance.SetPlayersMovable(true);
        GameManager.instance.SetBallMovable(true);
    }

    public void PlaySound()
    {
        AudioManager2D.instance.PlaySound("Event_GO", Camera.main.transform.position);
    }
}
