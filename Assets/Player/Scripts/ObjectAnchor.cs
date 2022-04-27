using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectAnchor : MonoBehaviour
{
    public UnityEvent ObjectGrabbed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            ObjectGrabbed.Invoke();
        }
    }
}
