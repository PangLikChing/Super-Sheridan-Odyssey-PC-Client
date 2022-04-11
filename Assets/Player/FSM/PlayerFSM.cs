using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerFSM : FSM_Q
{
    public readonly int ControllingStateName = Animator.StringToHash("Controlling");
    public readonly int SpawningStateName = Animator.StringToHash("Spawning");
    public readonly int VictoryStateName = Animator.StringToHash("Victory");
    public readonly int DefeatStateName = Animator.StringToHash("Defeat");
}
