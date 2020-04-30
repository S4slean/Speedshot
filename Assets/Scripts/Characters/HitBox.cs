using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	public TeamEnum team = TeamEnum.TEAM1;
	public Character player;

	public void Start()
	{
		team = player.team;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<Character>(out Character touchedPlayer))
		{
			if(touchedPlayer.team != team)
			{
				touchedPlayer.ReceiveDamage((int)Mathf.Sign(touchedPlayer.transform.position.x - transform.position.x));

				AudioManager2D.instance.PlaySound("Player_ChocTacle", transform.position);

				if (Random.Range(0, 1) > 0)
					AudioManager2D.instance.PlaySound("Event_Cheer", Camera.main.transform.position);
			}
		}
	}
}
