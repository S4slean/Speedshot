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

	private void Start()
	{

	}

	public void LaunchGame()
	{
		SceneManager.LoadScene("CityArena - Mathieu");
	}

	public void DebugLaunch()
	{
		SceneManager.LoadScene("CityArena - Mathieu Test");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			DebugLaunch();
			PlayerManager.instance.AllowPlayerToJoin(false);
		}
	}
}
