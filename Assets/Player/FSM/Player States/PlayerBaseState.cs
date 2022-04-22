using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerBaseState : FSMBaseState<PlayerFSM>
{
    protected PlayableDirector playableDirector;
    protected CharacterLocomotion playerMotion;
    protected CharacterAttack playerAttack;
    protected PlayerCameraControl cameraControl;
    protected InputHandler inputHandler;
    protected Animator characterAnimator;
    protected PlayerData playerData;
    
    public override void Init(GameObject _owner, FSM_Q _fsm)
    {
        base.Init(_owner, _fsm);
        inputHandler = PlayerController.Instance.inputHandler;
        characterAnimator = PlayerController.Instance.characterAnimation;
        playerAttack = PlayerController.Instance.characterAttack;
        playableDirector = PlayerController.Instance.playableDirector;
        playerMotion = PlayerController.Instance.characterLocomotion;
        cameraControl = PlayerController.Instance.cameraControl;
        playerData = PlayerController.Instance.playerData;
    }
}
