using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Win : MonoBehaviour
{
    public UnityEvent WinTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WinTrigger.Invoke();
            other.GetComponent<PlayerData>().isWinTriggerer = true;
            this.enabled = false;
        } 
        
    }
}
