using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using System;

public class LevelManager : Singleton<LevelManager>
{
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> avatarPrefabs = new List<GameObject>();
    public Transform instantiationPoint;
    public GameObject winTrigger;


    [HideInInspector]
    public UnityEvent Victory = new UnityEvent();
    [HideInInspector]
    public UnityEvent GameOver = new UnityEvent();
    [HideInInspector]
    public int playerIndex;

    //private GameObject[] playerAvatars;
    private PlayerData[] playerData;
    private int currentPlayerCount;
    private GameObject localPlayerAvatar;

    private enum GameState
    {
        PreGame,
        InGame,
        PostGame
    }
 
    private GameState gameState;
    private void Awake()
    {
        playerIndex = PhotonNetwork.IsMasterClient ? 0 : 1;
        localPlayerAvatar = PhotonNetwork.Instantiate(avatarPrefabs[playerIndex].name, instantiationPoint.position + Vector3.up * 0.05f, instantiationPoint.rotation);
        gameState = GameState.PreGame;
        //PlayerController.Instance.gameObject.SetActive(false);
        //UIManager.Instance.gameObject.SetActive(false);

    }

    void Start()
    {
        winTrigger.GetComponent<Win>().WinTrigger.AddListener(OnVictory);
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if ((string)p.CustomProperties["player_type"] == "PC")
            {
                currentPlayerCount++;
            }
        }
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.PreGame:
                PreGameUpdate();
                break;
            case GameState.InGame:
                InGameUpdate();
                break;
            case GameState.PostGame:
                PostGameUpdate();
                break;
        }

    }

    private void PreGameUpdate()
    {
        GameObject[] playerAvatars = GameObject.FindGameObjectsWithTag("Player");
        if (playerAvatars.Length != currentPlayerCount)
        {
            return;
        }
        else
        {
            playerData = new PlayerData[playerAvatars.Length];
            foreach (GameObject player in playerAvatars)
            {
                int index = Array.IndexOf(playerAvatars, player);
                playerData[index] = player.GetComponent<PlayerData>();
                playerData[index].PlayerExhausted.AddListener(OnPlayerExhausted);
            }
            PlayerController.Instance.localSpawnPoint = spawnPoints[playerIndex].transform;
            PlayerController.Instance.playerAvatar = localPlayerAvatar;
            PlayerController.Instance.enabled = true;
            UIManager.Instance.enabled = true;
            gameState = GameState.InGame;
        }
    }

    private void InGameUpdate()
    {
        if (currentPlayerCount == 0)
        {
            PlayerController.Instance.inputHandler.enabled = false;
            UIManager.Instance.gameObject.SetActive(false);
            GameOver.Invoke();
            gameState = GameState.PostGame;
        }
    }

    private void PostGameUpdate()
    {
        //
    }

    public void OnVictory()
    {
        PlayerController.Instance.inputHandler.enabled = false;
        UIManager.Instance.gameObject.SetActive(false);
        Victory.Invoke();
        gameState = GameState.PostGame;
    }

    public void OnPlayerExhausted()
    {
        currentPlayerCount--;
    }

    public GameObject GetActivePlayer() //return the first player avatar that is still active(not exhausted)
    {
        foreach (PlayerData data in playerData)
        {
            if (!data.isExhausted)
            {
                return data.gameObject;
            }
        }
        return null;
    }

    public GameObject GetWinTriggerer()
    {
        foreach (PlayerData data in playerData)
        {
            if (data.isWinTriggerer)
            {
                return data.gameObject;
            }
        }
        return null;
    }
}
