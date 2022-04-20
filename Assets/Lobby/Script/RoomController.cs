using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections.Generic;
using UnityEngine.UI;

public class RoomController : MonoBehaviourPunCallbacks
{
    //private List<RoomItem> roomItemsList = new List<RoomItem>();
    public GameObject startButton;
    public GameObject readyButton;
    public GameObject backToLobbyButton;
    public GameObject roomPanel;
    public PlayerItem playerItemPrefab;
    public Transform playerListContent;
    private int roomReady = 1;
    private Room currentRoom;
    private List<PlayerItem> playerItemsList = new List<PlayerItem>();
    private Hashtable playerCustomProperty = new Hashtable();
    public override void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        roomPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = PhotonNetwork.CurrentRoom.Name;
        currentRoom = PhotonNetwork.CurrentRoom;
        
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            playerCustomProperty.Add("player_status", 1);
            currentRoom.EmptyRoomTtl = 0;
        }
        else
        {
            readyButton.SetActive(true);
            playerCustomProperty.Add("player_status", 0);
        }
        playerCustomProperty.Add("player_type", "PC");
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperty);
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

    public void Ready()
    {
        playerCustomProperty["player_status"] = 1;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperty);
        backToLobbyButton.GetComponent<Button>().interactable = false;
    }

    public void BackToLobby()
    {
        playerCustomProperty.Clear();
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperty);
        PhotonNetwork.LeaveRoom(true);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            readyButton.SetActive(false);
        }
        UpdatePlayerListing();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        UpdatePlayerListing();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
    }

    private void UpdatePlayerListing()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();
        int readyCounter = 1;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            PlayerItem newPlayer = Instantiate(playerItemPrefab, playerListContent);
            newPlayer.SetPlayerName(player.NickName);
            newPlayer.SetPlayerType((string)player.CustomProperties["player_type"]);
            if (player.IsMasterClient)
            {
                newPlayer.SetPlayerStatus("Host");
            }
            else if ((int)player.CustomProperties["player_status"] == 1)
            {
                newPlayer.SetPlayerStatus("Ready");
            }
            else
            {
                newPlayer.SetPlayerStatus("");
            }
            readyCounter *= (int)player.CustomProperties["player_status"];
            playerItemsList.Add(newPlayer);
        }
        roomReady = readyCounter;
        if (PhotonNetwork.IsMasterClient && roomReady == 0)
        {
            startButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            startButton.GetComponent<Button>().interactable = true;
        }
    }
}
