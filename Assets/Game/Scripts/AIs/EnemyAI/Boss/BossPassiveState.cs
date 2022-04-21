using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPassiveState : BossBaseState
{
    NavMeshAgent navMeshAgent;
    float fallingRockCooldown = 0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize
        fallingRockCooldown = 0f;
        navMeshAgent = boss.GetComponent<NavMeshAgent>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the boss has a target
        if (boss.targetDetection.target != null)
        {
            // If the distance between the boss and the target is less than the runAwayDistance of the boss
            if (Vector3.Distance(boss.transform.position, boss.targetDetection.target.position) <= boss.runAwayDistance)
            {
                // Tell the boss to run to the bossENragePoint
                navMeshAgent.SetDestination(boss.bossEnragePoint.position);
            }
            // Else
            else
            {
                // Stand at where it is
                navMeshAgent.SetDestination(boss.transform.position);
            }
        }

        // If the boss has a target
        if (boss.targetDetection.target != null)
        {
            // Start droping rocks by enabling the fallingRocksPile
            boss.fallingRocks.gameObject.SetActive(true);

            // Cache the target's position
            Vector3 targetPosition = boss.targetDetection.target.position;

            // Set the falling rocks centre to the target
            boss.fallingRocks.position = new Vector3(targetPosition.x, 0, targetPosition.z);

            // If the fallingRockCooldown is up
            if (fallingRockCooldown >= boss.fallingRockTime)
            {
                // Enable a random falling rock
                PickARandomRock();

                // Reset fallingRockCooldown
                fallingRockCooldown = 0f;
            }
            else
            {
                // Increment fallingRockCooldown by deltaTime passed
                fallingRockCooldown += Time.deltaTime;
            }
        }

        // If the boss reached the bossEnragePoint
        if (boss.transform.position == boss.bossEnragePoint.position)
        {
            // Set trigger to transition to bossTransitionState
            animator.SetTrigger("Transition");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void PickARandomRock()
    {
        // Pick a random rock
        GameObject randomRock = boss.fallingRocks.GetChild(Random.Range(0, boss.fallingRocks.childCount)).gameObject;

        // If the rock is currently active
        if (randomRock.activeSelf == true)
        {
            // Pick another one
            PickARandomRock();
        }
        // Else
        else
        {
            // Enable that rock
            randomRock.SetActive(true);
        }
    }
}
