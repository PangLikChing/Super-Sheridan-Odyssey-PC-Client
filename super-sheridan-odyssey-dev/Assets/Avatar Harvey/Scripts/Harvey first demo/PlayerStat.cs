using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for managing player's stat
/// </summary>

public class PlayerStat : MonoBehaviour
{
    [SerializeField] float health = 3;

    void Start()
    {
        // Initialize
        health = 3;
    }

    public void TakeDamage(float damage)
    {
        // Player takes the damage
        health -= damage;
        Debug.Log("current health: " + health);

        // If the remaining health is less than or equal to 0
        if (health <= 0)
        {
            // Player is dead
            Debug.Log("Ah! I am dead!");
        }
    }
}
