using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rock : MonoBehaviour
{
    private PhotonView PV;
    public int damage = 1;
    public bool destroy = false;

    private void Start()
    {
        destroy = false;
        PV = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Enemy takes damage
            enemy.TakeDamage(damage);

            if (!PV.IsMine)
            {
                PV.TransferOwnership(PhotonNetwork.LocalPlayer);
            }

            // Destroy the gameObject across the network
            destroy = true;
        }
    }

    private void Update()
    {
        if (destroy && PV.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
