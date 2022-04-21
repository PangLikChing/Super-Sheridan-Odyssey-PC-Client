using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviourPun
{
    [SerializeField] Transform myAIPrefeb;

    void Start()
    {
        SpawnAI();
    }

    private void SpawnAI()
    {
        // If I am the master client
        if (PhotonNetwork.IsMasterClient)
        {
            // Spawn in the AI gameObject
            PhotonNetwork.Instantiate(myAIPrefeb.name, transform.position, Quaternion.identity);
        }
    }
}
