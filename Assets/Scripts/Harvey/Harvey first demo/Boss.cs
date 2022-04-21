using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(NavMeshAgent))]
public class Boss : MonoBehaviour
{
    // Initialize
    Enemy enemyStats;
    float bodyHitDamage = 1;

    public float transitionTime = 3f, fallingRockTime = 1.5f;

    public int regenHealth = 5;

    public Transform fallingRocks;

    public TargetDetection targetDetection;

    private void Update()
    {
        // Do something
    }
}
