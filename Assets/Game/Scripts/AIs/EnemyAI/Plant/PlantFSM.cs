using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Plant), typeof(Animator))]
public class PlantFSM : FSM
{
    [HideInInspector] public Animator _animator;
    [HideInInspector] public Plant plant;

    protected override void Awake()
    {
        plant = GetComponent<Plant>();
        _animator = plant.gameObject.GetComponent<Animator>();
        base.Awake();
    }
}
