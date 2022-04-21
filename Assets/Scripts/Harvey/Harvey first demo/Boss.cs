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

    public Transform bossEnragePoint;

    public float runAwayDistance = 7.5f;

    public float transitionTime = 3f, fallingRockTime = 1.5f;

    public int regenHealth = 5;

    [HideInInspector] public Transform fallingRocks;

    public TargetDetection targetDetection;

    void Start()
    {
        fallingRocks = FindObjectOfType<FallingRockPile>().transform;
        bossEnragePoint = FindObjectOfType<BossEnragePoint>().transform;
    }
}
