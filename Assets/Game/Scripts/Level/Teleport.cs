using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Teleport : MonoBehaviour
{
    public Transform theOtherPad;
    private bool teleported = false;
    private AudioSource teleportAudioSource;

    void Start()
    {
        teleportAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterLocomotion charMotion = other.GetComponent<CharacterLocomotion>();
        if (charMotion&&!teleported)
        {
            charMotion.enabled = false;
            charMotion.transform.position = theOtherPad.position + Vector3.up;
            //Debug.Log(charMotion.transform.position);
            teleported = true;
            teleportAudioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PhotonView>().IsMine)
        {
            CharacterLocomotion charMotion = other.GetComponent<CharacterLocomotion>();
            charMotion.enabled = true;
            teleported = false;
        }
    }
}
