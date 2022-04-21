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
    public GameObject unReadyButton;
    public GameObject backToLobbyButton;
    public GameObject roomPanel;
    public PlayerItem playerItemPrefab;
    public Transform playerListContent;
    private int roomReady = 1;
    private Room currentRoom;
    private List<PlayerItem> playerItemsList = new List<PlayerItem>();
    private Hashtable playerCustomProperty = new Hashtable();
    private Hashtable roomCustomProperty = null;
    public override void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        roomPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = PhotonNetwork.CurrentRoom.Name;
        currentRoom = PhotonNetwork.CurrentRoom;
        
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            readyButton.SetActive(false);
            unReadyButton.SetActive(false);
            playerCustomProperty.Add("player_status", 1);

            
            PhotonNetwork.CurrentRoom.EmptyRoomTtl = 0;
        }
        else
        {
            startButton.SetActive(false);
            readyButton.SetActive(true);
            unReadyButton.SetActive(false);
            playerCustomProperty.Add("player_status", 0);
            
        }
        roomCustomProperty = currentRoom.CustomProperties;
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
        readyButton.SetActive(false);
        unReadyButton.SetActive(true);
    }

    public void UnReady()
    {
        playerCustomProperty["player_status"] = 0;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperty);
        backToLobbyButton.GetComponent<Button>().interactable = true;
        unReadyButton.SetActive(false);
        readyButton.SetActive(true);
    }

    public void BackToLobby()
    {
        PhotonNetwork.LeaveRoom(true);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerListing();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            readyButton.SetActive(false);
            unReadyButton.SetActive(false);
            playerCustomProperty["player_status"] = 1;
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperty);
            backToLobbyButton.GetComponent<Button>().interactable = true;
        }
        UpdatePlayerListing();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        UpdatePlayerListing();
    }

    public override void OnLeftRoom()
    {
        playerCustomProperty.Clear();
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperty);
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
        int pcCount = 0;
        int mobileCount = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            PlayerItem newPlayer = Instantiate(playerItemPrefab, playerListContent);
            newPlayer.SetPlayerName(player.NickName);

            newPlayer.SetPlayerType((string)player.CustomProperties["player_type"]);
            if ((string)player.CustomProperties["player_type"] == "PC")
            {
                pcCount++;
            }
            else
            {
                mobileCount++;
            }

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

        if (PhotonNetwork.IsMasterClient)
        {
            roomCustomProperty["PC_Count"] = pcCount;
            roomCustomProperty["Mobile_Count"] = mobileCount;
            currentRoom.SetCustomProperties(roomCustomProperty);
        }
    }
}
