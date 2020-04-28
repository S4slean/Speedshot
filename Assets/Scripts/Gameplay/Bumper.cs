using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private SpriteRenderer sprite;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Player")
        {
            //TriggerBumper(col.GetComponent<Player>());
        }
    }

    /*public void TriggerBumper(PlayerPrefs affectedPlayer)
    {
        affectedPlayer.Bump();
    }*/

}
