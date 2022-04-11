using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for the bullet that the plant shots
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class RockBullet : MonoBehaviour
{
    // Initialize
    Vector3 direction;
    [SerializeField] float bulletDamage = 1;
    [SerializeField] float moveSpeed = 20f;

    void Awake()
    {
        // For safety
        // the bullet should be active in the scene for some reason. Can't debug yet
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // If the bullet is grabbed by a plant already
        if (transform.parent != null)
        {
            Transform plant = transform.parent;
            // offset is the distance between the bullet and the plant should be
            Vector3 offset = plant.forward;
            // Set the position of the bullet
            transform.position = plant.position + offset;
            // Turn the bullet to plant's
            transform.rotation = plant.rotation;
            // Remembers the original direction
            // Here assumes the plant has a target detection object
            direction = (plant.GetChild(0).GetComponent<TargetDetection>().target.position - transform.position).normalized;
        }
    }

    void FixedUpdate()
    {
        // Move the rock to the direction that they should go
        gameObject.transform.position += direction * Time.deltaTime * moveSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        // If the bullet hit the player
        if (other.GetComponent<PlayerData>() != null)
        {
            // It takes that damage
            other.GetComponent<PlayerData>().TakeDamage(bulletDamage);
        }

        // If the bullet hit the plant or the target detection collider
        if (other.GetComponent<Plant>() != null || other.GetComponent<TargetDetection>() != null)
        {
            // Do nothing
        }
        // If the bullet hit anything else
        else
        {
            // Disable the bullet
            gameObject.SetActive(false);
        }
    }
}
