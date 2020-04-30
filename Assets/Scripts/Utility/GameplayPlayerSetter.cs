using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPlayerSetter : MonoBehaviour, IPlayerSetter
{
    public GameObject playerPrefab;

    private List<GameObject> bluePlayers;
    private List<GameObject> redPlayer;

    private GameObject[] players;
    private Character[] characters;
    private CharacterInputHandler[] charactersInputHandler;

    public void SetupPlayer(int playerID)
    {
        players[playerID] = GameObject.Instantiate(playerPrefab);
        characters[playerID] = players[playerID].GetComponent<Character>();
        charactersInputHandler[playerID] = players[playerID].GetComponent<CharacterInputHandler>();

        //Inputs
        charactersInputHandler[playerID].SetPlayerInput(PlayerManager.instance.players[playerID].PlayerInput);

        //Character Settings
        characters[playerID].playerID = playerID;
        characters[playerID].team = PlayerManager.instance.players[playerID].PlayerTeam;
    }

    public void UnsetupPlayer(int playerID)
    {
        charactersInputHandler[playerID].UnsetPlayerInput();
    }
}
