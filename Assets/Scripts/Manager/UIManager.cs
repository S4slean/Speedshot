using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Screens")]
    public GameObject startGameScreen;
    public GameObject GoalScreen;
    public GameObject CountdownScreen;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void StartCountdown(int number)
    {
        StartCoroutine(Countdown(number));
    }

    IEnumerator Countdown(int startNumber)
    {
        int index = startNumber;
        while(index != 1)
        {
            //Set CountScreen
            Debug.Log(index);
            yield return new WaitForSeconds(1f);
            index--;
        }

        Time.timeScale = 1f;

        //Allow players and ball movement
        GameManager.instance.SetPlayersMovable(true);
        GameManager.instance.SetBallMovable(true);

        
    }
}
