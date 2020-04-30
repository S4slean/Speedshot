using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public static MainMenuManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(this.gameObject);
			return;
		}
		else
			instance = this;
	}

	public void LaunchGame()
	{
		SceneManager.LoadScene("CityArena - Mathieu");
		PlayerManager.instance.AllowPlayerToJoin(false);
	}
}
