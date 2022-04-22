using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script will be attached to the player controller

public class FSM_Q : MonoBehaviour
{
    public Animator fsmAnimator { get; private set; }

    // Start is called before the first frame update
    protected virtual void OnEnable()
    {
        fsmAnimator = GetComponent<Animator>();

        InternalFSMBaseState[] behaviours = fsmAnimator.GetBehaviours<InternalFSMBaseState>();
        foreach (var behaviour in behaviours)
        {
            behaviour.Init(gameObject, this);
        }
    }

    public bool ChangeState(string _stateName)
    {
        return ChangeState(Animator.StringToHash(_stateName));
    }

    public bool ChangeState(int _stateName)
    {
        bool hasState = true;
        fsmAnimator.CrossFade(_stateName, 0.0f);
        return hasState;
    }
}
