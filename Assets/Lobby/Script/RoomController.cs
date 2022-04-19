using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomController : MonoBehaviour
{
    //private List<RoomItem> roomItemsList = new List<RoomItem>();

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        PhotonNetwork.LoadLevel(1);
    }
}
