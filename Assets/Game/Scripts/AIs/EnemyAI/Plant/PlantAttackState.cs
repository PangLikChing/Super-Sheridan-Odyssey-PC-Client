using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAttackState : PlantBaseState
{
    // Initialize
    Enemy enemyStats;
    float attackDistance = 0, attackTimer = 0, attackInterval = 2, health = 0;
    TargetDetection targetDetection;
    Transform target;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize the stat at start
        enemyStats = plant.GetComponent<Enemy>();
        targetDetection = plant.targetDetection;
        attackDistance = enemyStats.attackDistance;
        attackInterval = plant.attackInterval;

        // Should I reset this? (game design question)
        attackTimer = 0;
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
            // Look at the target
            plant.transform.LookAt(target);

            // Calculate the distance between the player and this walking enemy
            float distance = Vector3.Distance(target.position, plant.transform.position);

            // If the distance between the player and this plant is less than the attacking distance and it's over the attack timer
            if (distance <= attackDistance && attackTimer >= attackInterval)
            {
                // Shoot the bullet
                plant.Attack();

                // Reset the attack timer
                attackTimer = 0;
            }
        }

        // Increase the attackTimer
        attackTimer += Time.deltaTime;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
