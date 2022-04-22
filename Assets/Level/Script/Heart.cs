using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Heart : MonoBehaviourPunCallbacks, IOnPhotonViewOwnerChange
{
    private PhotonView PV;

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
            Debug.Log(PV.IsMine);
            if (!PV.IsMine)
            {
                PV.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
            
        }
    }

    public void OnOwnerChange(Player newOwner, Player previousOwner)
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
