using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombIdleState : BombBaseState
{
    float health = 0;
    Enemy enemyStats;
    TargetDetection targetDetection;
    NavMeshAgent navMeshAgent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize
        targetDetection = bomb.targetDetection;
        enemyStats = bomb.GetComponent<Enemy>();
        navMeshAgent = bomb.GetComponent<NavMeshAgent>();

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

        // If there is currently no idleDestination
        if (bomb.idleDestination == null)
        {
            // Getting a random number between 0 to wanderingPoints that the bomb has
            int idleDestinationChildNumber = Random.Range(0, bomb.wanderingPoints.childCount);

            // Set bomb's idleDestination to whichever point got randommed
            bomb.idleDestination = bomb.wanderingPoints.GetChild(idleDestinationChildNumber);

            // Tell bomb to go to idleDestination
            navMeshAgent.SetDestination(bomb.idleDestination.position);
        }
        else
        {
            // Get the navMeshAgent's remainingDistance on the path
            float distance = navMeshAgent.remainingDistance;

            // If the bomb reaches the idleDestination and startedHangOut is false
            if (distance != Mathf.Infinity && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                navMeshAgent.remainingDistance == 0 && bomb.startedHangOut == false)
            {
                // Ask the bomb to hang out
                bomb.shouldHangOut = true;
            }
            else
            {
                navMeshAgent.SetDestination(bomb.idleDestination.position);
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // This currently doesn't work because the Coroutine is already started
        // Stop the hang out timmer if possible
        bomb.shouldHangOut = false;
    }
}
