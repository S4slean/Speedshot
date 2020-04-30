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
        if(col.TryGetComponent<Character>(out Character player))
        {
			player.SetInBump(true, this);
        }
        else if(col.transform.tag == "Ball")
        {
            TriggerBumper(col.GetComponent<Ball>());
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Character>(out Character player))
		{
			player.SetInBump(false, this);
		}
	}

	//public void TriggerBumper(Character affectedPlayer)
 //   {
 //       //Debug.Log("BUMP!");
 //       affectedPlayer.Bump(this);
 //   }

    public void TriggerBumper(Ball ball)
    {
        //Debug.Log("BUMP!");
        ball.Bump(this);
    }
}
