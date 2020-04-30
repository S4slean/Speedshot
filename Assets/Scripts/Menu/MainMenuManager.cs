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

    private void LaunchGame()
    {

    }
    
    public void DebugLaunch()
    {
        SceneManager.LoadScene("Test - Quentin 2");
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
