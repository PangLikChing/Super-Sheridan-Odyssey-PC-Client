using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBaseState : FSMBaseState
{
    protected Plant plant;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);

        PlantFSM plantFSM = (PlantFSM)fsm;
        plant = plantFSM.plant;
    }
}
