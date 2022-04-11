using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    [HideInInspector] public Transform target;

    private void OnTriggerStay(Collider other)
    {
        // If the plant doesn't have a target and the target has a PlayerStat component
        if (target == null && other.GetComponent<PlayerData>() != null)
        {
            // Set the target
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the plant has a target
        if (target != null)
        {
            // If the one left the attack range is the target
            if (target == other.transform)
            {
                // Set the target to null
                target = null;
            }
        }
    }
}
