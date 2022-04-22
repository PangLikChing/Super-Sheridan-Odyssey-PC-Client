using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RPCManager : MonoBehaviour
{
    PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        LevelManager.Instance.Victory.AddListener(GameWon);

        LevelManager.Instance.GameOver.AddListener(GameLost);
    }

    public void GameWon()
    {
        photonView.RPC("OnVictory", RpcTarget.Others);
    }

    public void GameLost()
    {
        photonView.RPC("OnLose", RpcTarget.Others);
    }
}
