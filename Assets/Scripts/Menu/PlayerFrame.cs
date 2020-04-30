using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrame : MonoBehaviour
{
    private PlayerFrameHandler playerFrameHandler;

    private void Awake()
    {
        playerFrameHandler = GetComponent<PlayerFrameHandler>();
    }

    public void OnPlayerAssigned()
    {

    }

    public void OnPlayerUnassigned()
    {

    }
}
