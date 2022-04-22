using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombChaseState : BombBaseState
{
    // Initialize
    Enemy enemyStats;
    float health = 0;
    TargetDetection targetDetection;
    NavMeshAgent navMeshAgent;
    Transform target;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize the stat at start
        enemyStats = bomb.GetComponent<Enemy>();
        targetDetection = bomb.targetDetection;
        navMeshAgent = bomb.GetComponent<NavMeshAgent>();

        // Save the original position of the bomb
        bomb.originalPosition = bomb.transform.position;
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
            // Get the target from the target detection trigger collider
            target = targetDetection.target;
        }
        // If the target is lost
        else
        {
            // Turn to idle state
            animator.SetTrigger("isIdle");

            // GTFO
            return;
        }

        // If the plant as a target
        if (target != null)
        {
            // If the distance between the target and the bomb is smaller than or equal to the maxChaseDistance
            if (Vector3.Distance(target.position, bomb.originalPosition) <= bomb.maxChaseDistance)
            {
                // Do some pathfinding to the target
                navMeshAgent.SetDestination(target.position);
            }
            // Else
            else
            {
                // Go back to the original location
                navMeshAgent.SetDestination(bomb.originalPosition);
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
