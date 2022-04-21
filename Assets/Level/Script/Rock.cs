using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rock : MonoBehaviour
{
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            //enemy.TakeDamage(1);
            if (!PV.IsMine)
            {
                PV.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
