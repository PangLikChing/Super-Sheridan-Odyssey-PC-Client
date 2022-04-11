using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PhotonView pv = other.GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            PlayerData stat = other.transform.parent.GetComponent<PlayerData>();
            if (stat != null)
            {
                stat.TakeDamage(stat.maxHealth);
            }
        }
    }
}
