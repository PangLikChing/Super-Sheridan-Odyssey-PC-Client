using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public float attackDistance = 10, bobyHitDamage = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
