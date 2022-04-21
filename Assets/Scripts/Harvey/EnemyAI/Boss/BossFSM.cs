using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boss), typeof(Animator))]
public class BossFSM : FSM
{
    [HideInInspector] public Animator _animator;
    [HideInInspector] public Boss boss;

    protected override void Awake()
    {
        boss = GetComponent<Boss>();
        _animator = boss.gameObject.GetComponent<Animator>();
        base.Awake();
    }
}
