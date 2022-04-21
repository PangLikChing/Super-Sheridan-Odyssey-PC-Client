using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Photon.Pun;
public class BreakOff : MonoBehaviour
{
    public float breakOffDelay = 2;
    private float breakOffCountDown;
    private bool isBreakingOff;
    private PhotonView PV;

    public AudioClip woodCreekClip;
    private AudioSource woodCreekSource;

    private void Start()
    {
        isBreakingOff = false;
        breakOffCountDown = breakOffDelay;
        woodCreekSource = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (isBreakingOff)
        {
            breakOffCountDown -= Time.deltaTime;
        }
        if (breakOffCountDown <= 0)
        {
            if (!PV.IsMine)
            {
                PV.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isBreakingOff = true;
            woodCreekSource.clip = woodCreekClip;
            woodCreekSource.Play();
        }
    }
}
