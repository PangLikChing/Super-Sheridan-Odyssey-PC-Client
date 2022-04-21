using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(NavMeshAgent))]
public class Boss : MonoBehaviour
{
    // Initialize
    float bodyHitDamage = 1;

    public Vector3 bossEnragePoint;

    public float passiveRockTime = 2.0f, enrageRockTime = 1.0f;

    public float runAwayDistance = 7.5f;

    public float transitionTime = 3f, fallingRockTime = 1.5f;

    public int regenHealth = 5;

    [HideInInspector] public Transform fallingRocks;

    public TargetDetection targetDetection;

    void Start()
    {
        // Initialize
        fallingRocks = FindObjectOfType<FallingRockPile>().transform;
        bossEnragePoint = FindObjectOfType<BossEnragePoint>().transform.position;

        bossEnragePoint = new Vector3(bossEnragePoint.x, transform.position.y, bossEnragePoint.z);
    }

    void OnTriggerEnter(Collider other)
    {
        // If the boss hits the player
        if (other.GetComponent<PlayerData>() != null)
        {
            other.GetComponent<PlayerData>().TakeDamage(bodyHitDamage);
        }
    }
}
