using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomController : MonoBehaviourPunCallbacks
{
    //private List<RoomItem> roomItemsList = new List<RoomItem>();
    public GameObject startButton;
    public GameObject readyButton;

    private Room currentRoom;

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            readyButton.SetActive(true);
        }
        currentRoom = PhotonNetwork.CurrentRoom;
        currentRoom.EmptyRoomTtl = 0;
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            currentRoom.IsOpen = false;
            currentRoom.IsVisible = false;
        }
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(1);
    }

    public void BackToLobby()
    {
        PhotonNetwork.LeaveRoom(true);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            readyButton.SetActive(false);
        }
    }
}
