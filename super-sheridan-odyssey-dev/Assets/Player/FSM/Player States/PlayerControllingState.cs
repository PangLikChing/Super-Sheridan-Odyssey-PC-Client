using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllingState : PlayerBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMotion.enabled = true;
        playerAttack.enabled = true;
        cameraControl.enabled = true;
        inputHandler.jump.AddListener(playerMotion.Jump);
        characterAnimator.SetBool("Control",true);
        playerData.PlayerDefeat.AddListener(OnPlayerDefeat);
        LevelManager.Instance.Victory.AddListener(OnPlayerVictory);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMotion.enabled = false;
        playerAttack.enabled = false;
        cameraControl.enabled = false;
        inputHandler.jump.RemoveListener(playerMotion.Jump);
        characterAnimator.SetBool("Control", false);
        playerData.PlayerDefeat.RemoveListener(OnPlayerDefeat);
        LevelManager.Instance.Victory.RemoveListener(OnPlayerVictory);
    }

    public void OnPlayerDefeat()
    {
        fsm.ChangeState(fsm.DefeatStateName);
    }

    public void OnPlayerVictory()
    {
        fsm.ChangeState(fsm.VictoryStateName);
    }
}
