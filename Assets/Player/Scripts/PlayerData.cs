using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    public float maxHealth = 3;
    private float currentHealth;
    public PhotonView PV;
    public UnityEvent<float> UpdateHealth;
    public UnityEvent PlayerDefeat;
    public UnityEvent PlayerExhausted;
    public bool isDead = false;
    public bool isExhausted = false;
    public bool isWinTriggerer = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        PV = GetComponent<PhotonView>();
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            if (damage < currentHealth)
            {
                currentHealth -= damage;
            }
            else
            {
                currentHealth = 0;
                isDead = true;
                PlayerDefeat.Invoke();
            }
            UpdateHealth.Invoke(currentHealth);
        }
    }

    public void RefillHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        UpdateHealth.Invoke(currentHealth);
    }

    [PunRPC]
    public void playerExhausted()
    {
        isExhausted = true;
        PlayerExhausted.Invoke();
    }
}
