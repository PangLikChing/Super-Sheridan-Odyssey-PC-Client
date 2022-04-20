using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictoryState : PlayerBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.Instance.stateCamera.m_Instructions[1].m_VirtualCamera.LookAt = LevelManager.Instance.GetWinTriggerer().transform;
        if (!playerData.isExhausted)
        {
            characterAnimator.SetBool("Victory", true);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
