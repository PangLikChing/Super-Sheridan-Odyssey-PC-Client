using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : FSMBaseState
{
    protected Boss boss;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);

        BossFSM bossFSM = (BossFSM)fsm;
        boss = bossFSM.boss;
    }
}
