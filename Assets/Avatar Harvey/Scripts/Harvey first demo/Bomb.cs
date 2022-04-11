using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(NavMeshAgent))]
public class Bomb : MonoBehaviour
{
    // Initialize
    Enemy enemyStats;
    float bodyHitDamage = 0;

    public TargetDetection targetDetection;

    [HideInInspector] public float attackInterval = 2, maxChaseDistance = 10;
    [HideInInspector] public Vector3 spawnPosition;

    void Start()
    {
        // Initialize the stat at start
        enemyStats = gameObject.GetComponent<Enemy>();
        bodyHitDamage = enemyStats.bobyHitDamage;

        // Get the original position
        spawnPosition = transform.position;
    }

    // Attacking
    //void OnCollisionEnter(Collision collision)
    //{
    //    // If the enemy hits the player
    //    if (collision.gameObject.GetComponent<PlayerData>() != null)
    //    {
    //        // Player takes bobyHitDamage amount of damage
    //        collision.gameObject.GetComponent<PlayerData>().TakeDamage(bodyHitDamage);

    //        // The plant takes 1 damage (damage should be determined by the player honestly. But we have to come up with a game design first)
    //        enemyStats.health -= 1;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        // If the enemy hits the player
        if (other.GetComponent<PlayerData>() != null)
        {
            // Player takes bobyHitDamage amount of damage
            other.GetComponent<PlayerData>().TakeDamage(bodyHitDamage);

            // The plant takes 1 damage (damage should be determined by the player honestly. But we have to come up with a game design first)
            enemyStats.health -= 1;
        }
    }
}
