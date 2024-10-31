using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Steamworks;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;
    public Text LobbyNameText;

    public GameObject PlayerListViewContent;
    public GameObject PlayerListItemPrefab;
    public GameObject LocalPlayerObject;

    public ulong CurrentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> PlayerListItems = new List<PlayerListItem>();
    public PlayerObjectController LocalPlayerController;

    public Button StartGameButton;
    public Text ReadyButtonText;



    private CustomNetworkManager manager;
    private CustomNetworkManager Manager
    {
        get
        {
            if(manager != null) return manager;
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public void ReadyPlayer()=>LocalPlayerController.ChangeReady();
    public void UpdateButton()
    {
        if(LocalPlayerController.Ready) ReadyButtonText.text = "해제";
        else ReadyButtonText.text = "준비";
    }
    public void CheckAllReady()
    {
        bool AllReady = true;
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            if(!player.Ready)
            {
                AllReady = false;
                break;
            }
        }
        if(AllReady)
        {
            if(LocalPlayerController.IsHost) StartGameButton.interactable = true;
            else StartGameButton.interactable = false;
        }
        else StartGameButton.interactable = false;
    }

    public void UpdateLobbyID()
    {
        CurrentLobbyID = Manager.CurrentLobbyID;
        // 로비 이름 짓는건 알아서 하셈
    }

    public void UpdatePlayerList()
    {
        if(!PlayerItemCreated) CreateHostPlayerItem();

        if(PlayerListItems.Count < Manager.GamePlayers.Count) CreateClientPlayerItem();
        if(PlayerListItems.Count > Manager.GamePlayers.Count) RemovePlayerItem();
        if(PlayerListItems.Count == Manager.GamePlayers.Count) UpdatePlayerItem();
    }

    public void FindLocalPlayer(PlayerObjectController playerObjectController)
    {
        LocalPlayerObject = playerObjectController.gameObject;
        LocalPlayerController = LocalPlayerObject.GetComponent<PlayerObjectController>();
    }

    public void CreateHostPlayerItem()
    {
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();
            
            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.Ready = player.Ready;
            NewPlayerItemScript.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            PlayerListItems.Add(NewPlayerItemScript);
        }
        PlayerItemCreated = true;
    }

    public void CreateClientPlayerItem()
    {
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            if(!PlayerListItems.Any(b => b.ConnectionID == player.ConnectionID))
            {
                GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
                PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();
                
                NewPlayerItemScript.PlayerName = player.PlayerName;
                NewPlayerItemScript.ConnectionID = player.ConnectionID;
                NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
                NewPlayerItemScript.Ready = player.Ready;
                NewPlayerItemScript.SetPlayerValues();

                NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                PlayerListItems.Add(NewPlayerItemScript);
            }
        }
    }

    public void UpdatePlayerItem()
    {
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            foreach(PlayerListItem PlayerListItemScript in PlayerListItems)
            {
                if(PlayerListItemScript.ConnectionID == player.ConnectionID)
                {
                    PlayerListItemScript.PlayerName = player.PlayerName;
                    PlayerListItemScript.Ready = player.Ready;
                    PlayerListItemScript.SetPlayerValues(); // 데이터의 내용을 실제 뷰에 적용
                    if(player == LocalPlayerController) UpdateButton();
                }
            }
        }
        CheckAllReady();
    }

    public void RemovePlayerItem()
    {
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();

        foreach(PlayerListItem playerListItem in PlayerListItems)
        {
            if(!Manager.GamePlayers.Any(b => b.ConnectionID == playerListItem.ConnectionID))
            {
                playerListItemToRemove.Add(playerListItem);
            }
        }
        if(playerListItemToRemove.Count > 0)
        {
            foreach(PlayerListItem itemToRemove in playerListItemToRemove)
            {
                GameObject objToRemove = itemToRemove.gameObject;
                PlayerListItems.Remove(itemToRemove);
                Destroy(objToRemove);
                objToRemove = null;
            }
        }
    }







    public void LeaveLobby()
    {
        SteamMatchmaking.LeaveLobby(new CSteamID(CurrentLobbyID));

        if(manager.mode == Mirror.NetworkManagerMode.Host) manager.StopHost();
        else manager.StopClient();

        SceneManager.LoadScene("Mainmenu");
    }
}
