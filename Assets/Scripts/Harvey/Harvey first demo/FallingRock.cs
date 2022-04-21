using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class FallingRock : MonoBehaviour
{
    [SerializeField] float fallingSpeed = 10f;
    [SerializeField] float originalHeight = 35f;
    [SerializeField] Transform fallingRocksPile;
    [SerializeField] float AreaOfEffectRadius = 5f;

    void OnEnable()
    {
        // Pop it out of the parent to fix the position
        transform.parent.parent = null;

        // Random a position around the target
        transform.parent.position = new Vector3(Random.Range(-AreaOfEffectRadius, AreaOfEffectRadius) + transform.parent.position.x, transform.parent.position.y, Random.Range(-AreaOfEffectRadius, AreaOfEffectRadius) + transform.parent.position.z);

        // Set the rock's height
        transform.position = new Vector3(transform.parent.position.x, originalHeight, transform.parent.position.z);

        // Reset Collider
        GetComponent<SphereCollider>().enabled = true;
    }

    void Update()
    {
        // Fall down in the rate of fallingSpeed per deltaTime
        transform.Translate(0, -fallingSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the rock hits anything other than an enemy or a target detection area
        if (other.GetComponent<Enemy>() == null && other.GetComponent<TargetDetection>() == null)
        {
            // Disable Collider
            GetComponent<SphereCollider>().enabled = false;

            // Reset parent to reset the position
            transform.parent.parent = fallingRocksPile;

            // Disable the whole Rock
            transform.parent.gameObject.SetActive(false);
        }
    }
}
