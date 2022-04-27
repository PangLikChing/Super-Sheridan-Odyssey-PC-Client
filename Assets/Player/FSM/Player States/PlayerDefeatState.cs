using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefeatState : PlayerBaseState
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.Instance.Spawn.AddListener(OnDeathSceneEnd);
        characterAnimator.SetTrigger("Killed");
        LevelManager.Instance.Victory.AddListener(OnPlayerVictory);
        playerAttack.DetachObject();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.Instance.Spawn.RemoveListener(OnDeathSceneEnd);
        LevelManager.Instance.Victory.RemoveListener(OnPlayerVictory);
    }

    public void OnDeathSceneEnd()
    {
        fsm.ChangeState(fsm.SpawningStateName);
    }

    public void OnPlayerVictory()
    {
        fsm.ChangeState(fsm.VictoryStateName);
    }
}
