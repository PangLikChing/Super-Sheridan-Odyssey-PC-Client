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
            Instantiate(myAIPrefeb, transform.position, Quaternion.identity);
        }
    }
}
