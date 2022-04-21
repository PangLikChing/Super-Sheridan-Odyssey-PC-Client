using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBaseState : FSMBaseState
{
    protected Bomb bomb;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);

        BombFSM preditorFSM = (BombFSM)fsm;
        bomb = preditorFSM.bomb;
    }
}
