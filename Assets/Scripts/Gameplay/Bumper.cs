using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private SpriteRenderer sprite;

    [Header("Bumping")]
    public AnimationCurve bumpCurve;
    public float bumpForce = 5;
    public float bumpMaxDuration;
    public float bumpReleaseFactor = 2f;
    [HideInInspector] public float bumpTracker;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Player")
        {
            TriggerBumper(col.GetComponent<Character>());
        }
    }

    public void TriggerBumper(Character affectedPlayer)
    {
        Debug.Log("BUMP!");
        affectedPlayer.Bump(this);
    }

}
