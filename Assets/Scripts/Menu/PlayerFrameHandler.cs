using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrameHandler : MonoBehaviour, IPlayerSetter
{
    public PlayerFrame[] playersFrame = new PlayerFrame[4];

    public void SetupPlayer(int playerID)
    {
        throw new System.NotImplementedException();
    }

    public void UnsetupPlayer(int playerID)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
