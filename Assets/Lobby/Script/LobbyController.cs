using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class LobbyController : MonoBehaviourPunCallbacks
{
    private float nextUpdateTime;
    private string createRoomName = null;
    private List<RoomItem> roomItemsList = new List<RoomItem>();
    private Hashtable roomCustomProperty = new Hashtable();
    private int maxPCPlayer = 2;
    [HideInInspector]
    public float timeBetweenUpdates = 1.5f;
    public string selectRoomName = null;
    public Transform contentObject;
    public RoomItem roomItemPrefab;
    public GameObject lobbyPanel;

    public GameObject roomNamePanel;
    public StatusMsg statusMsg;

    public void CreateRoom()
    {
        roomNamePanel.SetActive(true);
        roomNamePanel.transform.GetChild(2).GetComponent<TMP_InputField>().text = "";
    }

    public void SetRoomName(TMP_InputField input)
    {
        createRoomName = input.text;
    }

    public void Create()
    {
        if (!string.IsNullOrEmpty(createRoomName))
        {
            foreach (RoomItem item in roomItemsList)
            {
                if (createRoomName == item.roomName.text)
                {
                    statusMsg.SetStatusMsg("Duplicate room name detected");
                    return;
                }
            }

            roomCustomProperty.Add("PC_Count", 1);
            roomCustomProperty.Add("Mobile_Count", 0);
            string[] customPropertyForlobby = { "PC_Count", "Mobile_Count" };
            RoomOptions options = new RoomOptions()
            {
                IsOpen = true,
                IsVisible = true,
                MaxPlayers = 3,
                EmptyRoomTtl = 0,
                CustomRoomProperties = roomCustomProperty,
                CustomRoomPropertiesForLobby = customPropertyForlobby
            };
            PhotonNetwork.CreateRoom(createRoomName, options);
        }
        else
        {
            statusMsg.SetStatusMsg("Room name is required");
        }

    }

    public void Cancel()
    {
        roomNamePanel.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        roomNamePanel.SetActive(false);
        lobbyPanel.SetActive(false);
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();
        Debug.Log("Joined: " + PhotonNetwork.CurrentRoom.Name);
    }

    public void JoinRoom()
    {
        if (!string.IsNullOrEmpty(selectRoomName))
        {
            PhotonNetwork.JoinRoom(selectRoomName);
            selectRoomName = null;

        }
        else
        {
            statusMsg.SetStatusMsg("You need to select a room first");
        }

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            int index = roomItemsList.FindIndex(x => x.roomName.text == room.Name);
            if (index == -1)
            {
                if (!room.RemovedFromList && (int)room.CustomProperties["PC_Count"] < maxPCPlayer)
                {
                    RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
                    newRoom.SetRoomName(room.Name);
                    roomItemsList.Add(newRoom);
                }
            }
            else
            {
                if (room.RemovedFromList || (int)room.CustomProperties["PC_Count"] >= maxPCPlayer)
                {
                    Destroy(roomItemsList[index].gameObject);
                    roomItemsList.RemoveAt(index);
                }
            }
        }
    }

    public override void OnLeftRoom()
    {
        roomCustomProperty.Clear();
        if (lobbyPanel != null)
            lobbyPanel.SetActive(true);
    }

}
