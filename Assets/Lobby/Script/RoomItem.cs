using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomName;
    private LobbyController controller;

    private void Start()
    {
        controller = FindObjectOfType<LobbyController>();
        Debug.Log(controller.gameObject.name);
    }
    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void SelectRoom()
    {
        controller.selectRoomName = roomName.text;
        Debug.Log(roomName.text);
    }
}
