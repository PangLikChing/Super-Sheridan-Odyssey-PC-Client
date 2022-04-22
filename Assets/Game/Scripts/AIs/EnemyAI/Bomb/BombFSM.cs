using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bomb), typeof(Animator))]
public class BombFSM : FSM
{
    [HideInInspector] public Animator _animator;
    [HideInInspector] public Bomb bomb;

    protected override void Awake()
    {
        bomb = GetComponent<Bomb>();
        _animator = bomb.gameObject.GetComponent<Animator>();
        base.Awake();
    }
}