using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PlayerData data = other.GetComponent<PlayerData>();
        if (data != null)
        {
            data.TakeDamage(3);
        }
    }
}
