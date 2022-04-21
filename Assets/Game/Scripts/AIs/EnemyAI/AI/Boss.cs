using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(NavMeshAgent))]
public class Boss : MonoBehaviour
{
    // Initialize
    float bodyHitDamage = 1;
    Enemy enemyStats;

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
        enemyStats = GetComponent<Enemy>();
        fallingRocks = FindObjectOfType<FallingRockPile>().transform;
        bossEnragePoint = FindObjectOfType<BossEnragePoint>().transform.position;

        bossEnragePoint = new Vector3(bossEnragePoint.x, transform.position.y, bossEnragePoint.z);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // If the enemy hits the player
        if (hit.gameObject.GetComponent<PlayerData>() != null)
        {
            // Player takes bobyHitDamage amount of damage
            hit.gameObject.GetComponent<PlayerData>().TakeDamage(bodyHitDamage);

            // The boss takes 1 damage (damage should be determined by the player honestly. But we have to come up with a game design first)
            enemyStats.health -= 1;
        }
    }
}
