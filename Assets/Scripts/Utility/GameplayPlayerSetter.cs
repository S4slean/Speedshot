using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class GameplayPlayerSetter : MonoBehaviour, IPlayerSetter
{
    public GameObject bluePlayerPrefab;
    public GameObject redPlayerPrefab;

    private List<GameObject> bluePlayers;
    public List<GameObject> BluePlayers { get => bluePlayers; }
    private List<GameObject> redPlayers;
    public List<GameObject> RedPlayers { get => redPlayers; }

    private GameObject[] players;
    private Character[] characters;
    private CharacterInputHandler[] charactersInputHandler;

    private void Awake()
    {
        bluePlayers = new List<GameObject>();
        redPlayers = new List<GameObject>();

        players = new GameObject[4];
        characters = new Character[4];
        charactersInputHandler = new CharacterInputHandler[4];
    }

    public void StartSetup()
    {
        SetAsPlayerSetter();
        PlayerManager.instance.SetupPlayers();
        //Debug.Log(BlueCaracters().Count);
        //Debug.Log(RedCaracters().Count);
        GameManager.instance.blueTeam = BlueCaracters();
        GameManager.instance.redTeam = RedCaracters();
    }

    public void SetupPlayer(int playerID)
    {
        switch (PlayerManager.instance.players[playerID].PlayerTeam)
        {
            case TeamEnum.TEAM1:
                players[playerID] = (GameObject.Instantiate(bluePlayerPrefab));
                bluePlayers.Add(players[playerID]);
                break;
            case TeamEnum.TEAM2:
                players[playerID] = GameObject.Instantiate(redPlayerPrefab);
                redPlayers.Add(players[playerID]);
                break;
            default:
                return;
        }

        players[playerID].SetActive(false);
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

    public void SetAsPlayerSetter()
    {
        PlayerManager.instance.PlayerSetter = this;
    }

    private List<Character> BlueCaracters()
    {
        List<Character> blueCharacters = new List<Character>();

        foreach(Character character in characters)
        {
            if (character != null && character.team == TeamEnum.TEAM1)
                blueCharacters.Add(character);
        }

        return blueCharacters;
    }

    private List<Character> RedCaracters()
    {
        List<Character> redCharacters = new List<Character>();

        foreach (Character character in characters)
        {
            if (character != null && character.team == TeamEnum.TEAM2)
                redCharacters.Add(character);
        }

        return redCharacters;
    }
}
