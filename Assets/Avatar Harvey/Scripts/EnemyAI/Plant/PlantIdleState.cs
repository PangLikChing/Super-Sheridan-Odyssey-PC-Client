using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantIdleState : PlantBaseState
{
    float health = 0;
    Enemy enemyStats;
    TargetDetection targetDetection;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize
        targetDetection = plant.targetDetection;
        enemyStats = plant.GetComponent<Enemy>();

        // Play the idle animation
        // The animation should be a looping one
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the current health
        health = enemyStats.health;

        // If plant's health is less than or equal to 0
        if (health <= 0)
        {
            // To dead state
            animator.SetTrigger("isDead");
        }

        // If there is a target
        if (targetDetection.target != null)
        {
            // Turn to attack state
            animator.SetTrigger("hasTarget");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
