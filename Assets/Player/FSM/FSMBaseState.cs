using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base state for all FSM states
/// </summary>
public abstract class FSMBaseState<T> : InternalFSMBaseState where T : FSM_Q
{
    protected GameObject owner { get; private set; }
    protected T fsm { get; private set; }

    public override void Init(GameObject _owner, FSM_Q _fsm)
    {
        owner = _owner;
        fsm = (T)_fsm;
    }
}
