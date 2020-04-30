using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanelAnim : MonoBehaviour
{
    public void StartEndAnim()
    {
        GetComponent<Animator>().SetTrigger("EndGame");
    }

    public void PlaySound()
    {
        AudioManager2D.instance.PlaySound("Event_GO", Camera.main.transform.position);
    }

}
