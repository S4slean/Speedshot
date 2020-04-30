using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrame : MonoBehaviour
{
    [HideInInspector] public MenuInputHandler menuInputHandler;

    private void Awake()
    {
        menuInputHandler = GetComponent<MenuInputHandler>();
    }

    public void OnPlayerAssigned()
    {

    }

    public void OnPlayerUnassigned()
    {

    }
}
