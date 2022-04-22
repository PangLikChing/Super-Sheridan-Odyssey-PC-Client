using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RPCManager : MonoBehaviour
{
    public UnityEvent gameWon;
    public UnityEvent gameLost;
    public const byte EventCode = 0;
    private void Start()
    {
        LevelManager.Instance.Victory.AddListener(GameWon);

        LevelManager.Instance.GameOver.AddListener(GameLost);
    }

    public void GameWon()
    {
        PhotonNetwork.RaiseEvent(EventCode, 0, null, SendOptions.SendReliable);
    }

    public void GameLost()
    {
        PhotonNetwork.RaiseEvent(EventCode, 1, null, SendOptions.SendReliable);
    }
}
