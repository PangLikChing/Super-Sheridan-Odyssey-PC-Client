using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for the plants' behaviour in the game scene
/// </summary>

[RequireComponent(typeof(Enemy))]
public class Plant : MonoBehaviour
{
    // Initialize
    Enemy enemyStats;
    float bodyHitDamage = 0;
    int originalChildNumber = 0;

    public TargetDetection targetDetection;

    [SerializeField] Transform bulletPile;
    [HideInInspector] public float attackInterval = 2;

    void Start()
    {
        // Initialize the stat at start
        enemyStats = gameObject.GetComponent<Enemy>();
        bodyHitDamage = enemyStats.bobyHitDamage;
        originalChildNumber = transform.childCount - 1;
        bulletPile = FindObjectOfType<BulletPile>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the enemy hits the player
        if (other.gameObject.GetComponent<PlayerData>() != null)
        {
            Debug.Log("yo");

            // Player takes bobyHitDamage amount of damage
            other.gameObject.GetComponent<PlayerData>().TakeDamage(bodyHitDamage);

            // The plant takes max health amount of damage (damage should be determined by the player honestly. But we have to come up with a game design first)
            enemyStats.TakeDamage(enemyStats.health);

            // To dead state
            GetComponent<Animator>().SetTrigger("isDead");
        }
    }

    // Function to attack
    public void Attack()
    {
        // If the plant doesn't have any bullet
        if (transform.childCount == originalChildNumber + 1)
        {
            // Refill the bullet
            // If bulletPile still has bullet
            if (bulletPile.childCount != 0)
            {
                // Get a bullet from the bullet pile
                bulletPile.GetChild(0).transform.SetParent(transform);

                // Disable that bullet
                transform.GetChild(originalChildNumber + 1).gameObject.SetActive(false);
            }
        }

        // Shot the first bullet
        transform.GetChild(originalChildNumber + 1).gameObject.SetActive(true);

        // Set the parent of that bullet to the bullet pile
        transform.GetChild(originalChildNumber + 1).SetParent(bulletPile);
    }
}
