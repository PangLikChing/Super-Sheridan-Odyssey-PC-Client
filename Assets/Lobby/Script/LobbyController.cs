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
    [HideInInspector]
    public float timeBetweenUpdates = 1.5f;
    public string selectRoomName = null;
    public Transform contentObject;
    public RoomItem roomItemPrefab;
    public GameObject lobbyPanel;

    public GameObject roomNamePanel;
    public StatusMsg statusMsg;

    private void Start()
    {

    }

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
        Debug.Log(createRoomName);
        if (createRoomName != null && createRoomName.Length > 0)
        {
            roomCustomProperty.Add("PC_Count", 1);
            roomCustomProperty.Add("Mobile_Count", 0);
            string[] customPropertyForlobby = { "PC_Count", "Mobile_Count" };
            RoomOptions options = new RoomOptions() { 
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
        
        Debug.Log("Joined: " + PhotonNetwork.CurrentRoom.Name);
    }

    public void JoinRoom()
    {
        if (selectRoomName != null && selectRoomName.Length > 0 )
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
        
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }

        roomItemsList.Clear();

        foreach (RoomInfo room in roomList)
        {
            if (!room.RemovedFromList&&(int)room.CustomProperties["PC_Count"] < 2)
            {
                RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
                newRoom.SetRoomName(room.Name);
                roomItemsList.Add(newRoom);
            }
        }
    }

    public override void OnLeftRoom()
    {
        if(lobbyPanel!=null)
        lobbyPanel.SetActive(true);
    }
}
