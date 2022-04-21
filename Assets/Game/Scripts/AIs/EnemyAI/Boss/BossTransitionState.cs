using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTransitionState : BossBaseState
{
    float transitionTime = 0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize
        transitionTime = 0f;

        // Reset health to max
        boss.GetComponent<Enemy>().health = boss.regenHealth;

        // Disable the boss component on the boss to make it invunlable
        boss.GetComponent<Boss>().enabled = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the transition time is up
        if (transitionTime >= boss.transitionTime)
        {
            // Set Enraged trigger to true
            animator.SetTrigger("Enraged");
        }
        else
        {
            // Increment the time by deltaTime passed
            transitionTime += Time.deltaTime;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Enable the boss component on the boss to make it able to take damage
        boss.GetComponent<Boss>().enabled = true;
    }
}
