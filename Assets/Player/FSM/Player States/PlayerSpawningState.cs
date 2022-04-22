using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerSpawningState : PlayerBaseState
{
    public bool enabled;
    private bool spawned;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spawned = false;
        characterAnimator.SetBool("Spawn", true);
        playableDirector.stopped += OnPlayerSpawned;
        playerData.RefillHealth();
        playableDirector.Play();
        LevelManager.Instance.Victory.AddListener(OnPlayerVictory);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Finish Camera work then transition to controlling state
        if (spawned||!enabled)
        {
            fsm.ChangeState(fsm.ControllingStateName);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        characterAnimator.SetBool("Spawn", false);
        playableDirector.stopped -= OnPlayerSpawned;
        LevelManager.Instance.Victory.RemoveListener(OnPlayerVictory);
    }

    public void OnPlayerSpawned(PlayableDirector aDirector)
    {
        spawned = true;
    }

    public void OnPlayerVictory()
    {
        fsm.ChangeState(fsm.VictoryStateName);
    }
}
