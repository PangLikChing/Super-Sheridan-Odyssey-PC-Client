using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Heart : MonoBehaviour
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
            if (!PV.IsMine)
            {
                PV.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
