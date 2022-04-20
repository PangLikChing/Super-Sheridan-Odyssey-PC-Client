using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float damageValue = 1;
    private void OnTriggerEnter(Collider other)
    {
        PhotonView pv = other.GetComponent<PhotonView>();
        if (pv!=null&&pv.IsMine)
        {
            PlayerData data = other.GetComponent<PlayerData>();
            if (data != null)
            {
                data.TakeDamage(damageValue);
            }
        }
    }
}
