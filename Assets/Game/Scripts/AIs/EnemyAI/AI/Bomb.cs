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

    public Transform wanderingPoints;
    public TargetDetection targetDetection;
    public float maxChaseDistance = 10;

    [HideInInspector] public bool shouldHangOut = false, startedHangOut = false;
    [HideInInspector] public Transform idleDestination;
    [HideInInspector] public Vector3 originalPosition;

    void Start()
    {
        // Initialize the stat at start
        enemyStats = gameObject.GetComponent<Enemy>();
        bodyHitDamage = enemyStats.bobyHitDamage;
        shouldHangOut = false;
        startedHangOut = false;
        wanderingPoints = GameObject.Find("Wandering Points").transform;
    }

    void FixedUpdate()
    {
        // Check if the bomb should start hanging out at the idle destination
        if (shouldHangOut == true)
        {
            // Reset shouldHangOut
            shouldHangOut = false;

            // Set started hangout to true
            startedHangOut = true;

            // Start hanging out
            StartCoroutine(hangingOut(2.0f));
        }
    }

    // Attacking
    private void OnTriggerEnter(Collider other)
    {
        // If the enemy hits the player
        if (other.gameObject.GetComponent<PlayerData>() != null)
        {
            // Player takes bobyHitDamage amount of damage
            other.gameObject.GetComponent<PlayerData>().TakeDamage(bodyHitDamage);

            // The plant takes 1 damage (damage should be determined by the player honestly. But we have to come up with a game design first)
            enemyStats.health -= 1;
        }
    }

    // IEnumerator for hanging out at the idleDestination
    private IEnumerator hangingOut(float idleTime)
    {
        // Wait for idleTime amount of seconds
        yield return new WaitForSeconds(idleTime);

        // Set bomb's idleDestination to null so that the bomb will pick a new idleDestination
        idleDestination = null;
        
        // Reset startedHangOut so that the idle state won't ask for more hang outs
        startedHangOut = false;
    }
}
