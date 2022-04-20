using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Coin : MonoBehaviour
{
    public int coinValue;
    public AudioClip coinClip;
    private void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        PhotonView pv = other.GetComponent<PhotonView>();

        PlayerData data = other.GetComponent<PlayerData>();
        if (data != null)
        {
            AudioManager.Instance.PlayClip(coinClip);
            if (pv.IsMine)
            {
                PlayerController.Instance.KeepScore(coinValue);
            }
            Destroy(gameObject);
        }
    }
}
