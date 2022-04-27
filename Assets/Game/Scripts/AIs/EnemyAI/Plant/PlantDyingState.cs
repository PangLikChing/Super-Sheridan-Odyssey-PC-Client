using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDyingState : PlantBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Play the dying animation
        // The animation should be a one time animation

        Debug.Log("Ahh the plant is dead");

        // Destroy the gameObject
        PhotonNetwork.Destroy(plant.gameObject);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
