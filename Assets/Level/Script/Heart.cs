using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Heart : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    private bool destory;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerData data = other.GetComponent<PlayerData>();
        if (data != null)
        {
            data.RefillHealth();
            if (!PV.IsMine)
            {
                PV.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
            destory = true;
        }
    }

    private void Update()
    {
        if (destory && PV.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
