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
    public GameObject BlindUserListContent;
    public GameObject LimpUserListContent;

    public GameObject PlayerListItemPrefab;
    public GameObject PlayerBlindRoleItemPrefab;
    public GameObject PlayerLimpRoleItemPrefab;
    
    
    public GameObject LocalPlayerObject;

    public ulong CurrentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> PlayerListItems = new List<PlayerListItem>();
    private List<PlayerRoleItem> BlindUserList = new List<PlayerRoleItem>();
    private List<PlayerRoleItem> LimpUserList = new List<PlayerRoleItem>();

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
    public void ChangeRole(int roleCode)=>LocalPlayerController.ChangeRole(roleCode);
    public void UpdateButton()
    {
        if(LocalPlayerController.Ready) ReadyButtonText.text = "해제";
        else ReadyButtonText.text = "준비";
    }
    public void CheckAllReady()
    {
        bool AllReady = true;
        int blindCount = 0;
        int LimpCount = 0;

        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            if(player.Role == PlayerObjectController.Blind) blindCount++;
            else if(player.Role == PlayerObjectController.Limp) LimpCount++;

            if(!player.Ready)
            {
                AllReady = false;
                break;
            }
        }
        if(blindCount != 1 || LimpCount != 1) AllReady = false;
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
            
            GameObject NewBlindRoleItemGO = Instantiate(PlayerBlindRoleItemPrefab) as GameObject; 
            PlayerRoleItem NewBlindRoleItem = NewBlindRoleItemGO.GetComponent<PlayerRoleItem>();
            GameObject NewLimpRoleItemGO = Instantiate(PlayerLimpRoleItemPrefab) as GameObject; 
            PlayerRoleItem NewLimpRoleItem = NewLimpRoleItemGO.GetComponent<PlayerRoleItem>();
            
            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.Ready = player.Ready;
            NewPlayerItemScript.SetPlayerValues();

            NewBlindRoleItem.playerName = player.PlayerName;
            NewBlindRoleItem.ConnectionID = player.ConnectionID;
            NewBlindRoleItem.SetPlayerValues();
            NewLimpRoleItem.playerName = player.PlayerName;
            NewLimpRoleItem.ConnectionID = player.ConnectionID;
            NewLimpRoleItem.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            NewBlindRoleItem.transform.SetParent(BlindUserListContent.transform);
            NewBlindRoleItem.transform.localScale = Vector3.one;
            NewLimpRoleItem.transform.SetParent(LimpUserListContent.transform);
            NewLimpRoleItem.transform.localScale = Vector3.one;

            NewBlindRoleItem.gameObject.SetActive(false);
            NewLimpRoleItem.gameObject.SetActive(false);
        
            BlindUserList.Add(NewBlindRoleItem);
            LimpUserList.Add(NewLimpRoleItem);
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
                
                GameObject NewBlindRoleItemGO = Instantiate(PlayerBlindRoleItemPrefab) as GameObject; 
                PlayerRoleItem NewBlindRoleItem = NewBlindRoleItemGO.GetComponent<PlayerRoleItem>();
                GameObject NewLimpRoleItemGO = Instantiate(PlayerLimpRoleItemPrefab) as GameObject; 
                PlayerRoleItem NewLimpRoleItem = NewLimpRoleItemGO.GetComponent<PlayerRoleItem>();
                
                NewPlayerItemScript.PlayerName = player.PlayerName;
                NewPlayerItemScript.ConnectionID = player.ConnectionID;
                NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
                NewPlayerItemScript.Ready = player.Ready;
                NewPlayerItemScript.SetPlayerValues();

                NewBlindRoleItem.playerName = player.PlayerName;
                NewBlindRoleItem.ConnectionID = player.ConnectionID;
                NewBlindRoleItem.SetPlayerValues();
                NewLimpRoleItem.playerName = player.PlayerName;
                NewLimpRoleItem.ConnectionID = player.ConnectionID;
                NewLimpRoleItem.SetPlayerValues();

                NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                NewBlindRoleItem.transform.SetParent(BlindUserListContent.transform);
                NewBlindRoleItem.transform.localScale = Vector3.one;
                NewLimpRoleItem.transform.SetParent(LimpUserListContent.transform);
                NewLimpRoleItem.transform.localScale = Vector3.one;
            
                BlindUserList.Add(NewBlindRoleItem);
                LimpUserList.Add(NewLimpRoleItem);
                PlayerListItems.Add(NewPlayerItemScript);
            }
        }
    }

    public void UpdatePlayerItem()
    {
        Debug.Log("LobbyController : Update Called!");
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

            foreach(PlayerRoleItem item in BlindUserList)
            {
                if(item.ConnectionID == player.ConnectionID)
                {
                    item.playerName = player.PlayerName;
                    item.SetPlayerValues();
                    Debug.Log("Player.Role : " + player.Role + ", and this is BlindUserList.");
                    if(player.Role != PlayerObjectController.Blind) item.ChangeRoleNotActive();
                    if(player.Role == PlayerObjectController.Blind) item.ChangeRoleActive();
                }
            }
            foreach(PlayerRoleItem item in LimpUserList)
            {
                if(item.ConnectionID == player.ConnectionID)
                {
                    item.playerName = player.PlayerName;
                    item.SetPlayerValues();
                    Debug.Log("Player.Role : " + player.Role + ", and this is LimpUserList.");
                    if(player.Role != PlayerObjectController.Limp) item.ChangeRoleNotActive();
                    if(player.Role == PlayerObjectController.Limp)
                    {
                        Debug.Log("player.Role : " + player.Role + ", Limp값 : " + PlayerObjectController.Limp + "이 같습니다!");
                        item.ChangeRoleActive();
                    } 
                }
            }
        }
        CheckAllReady();
    }

    public void RemovePlayerItem()
    {
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();
        List<PlayerRoleItem> playerBlindRoleItemToRemove = new List<PlayerRoleItem>();
        List<PlayerRoleItem> playerLimpRoleItemToRemove = new List<PlayerRoleItem>();

        foreach(PlayerListItem playerListItem in PlayerListItems)
        {
            if(!Manager.GamePlayers.Any(b => b.ConnectionID == playerListItem.ConnectionID))
            {
                playerListItemToRemove.Add(playerListItem);
            }
        }
        foreach(PlayerRoleItem item in BlindUserList)
        {
            if(!Manager.GamePlayers.Any(b => b.ConnectionID == item.ConnectionID))
            {
                playerBlindRoleItemToRemove.Add(item);
            }
        }
        foreach(PlayerRoleItem item in LimpUserList)
        {
            if(!Manager.GamePlayers.Any(b => b.ConnectionID == item.ConnectionID))
            {
                playerLimpRoleItemToRemove.Add(item);
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
        if(playerBlindRoleItemToRemove.Count > 0)
        {
            foreach(PlayerRoleItem itemToRemove in playerBlindRoleItemToRemove)
            {
                GameObject objToRemove = itemToRemove.gameObject;
                BlindUserList.Remove(itemToRemove);
                Destroy(objToRemove);
                objToRemove = null;
            }
        }
        if(playerLimpRoleItemToRemove.Count > 0)
        {
            foreach(PlayerRoleItem itemToRemove in playerLimpRoleItemToRemove)
            {
                GameObject objToRemove = itemToRemove.gameObject;
                LimpUserList.Remove(itemToRemove);
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

        UpdatePlayerList();

        SceneManager.LoadScene("Mainmenu");
    }

    public void StartGame(string SceneName)
    {
        LocalPlayerController.CanStartGame(SceneName);
    }


}
