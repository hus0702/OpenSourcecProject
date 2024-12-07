using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.InputSystem;
using Unity.Jobs;
using Mirror.Examples.Common;

/*
    이 클래스는 접속한 각각의 플레이어의 정보를 담고, 통신을 하게 될 객체임.

    플레이어의 정보를 갱신한다 == PlayerObjectController 의 정보를 갱신한다.
*/
public class PlayerObjectController : NetworkBehaviour
{
    public const int Null = 0;
    public const int Blind = 1;
    public const int Limp = 2;
    /*
        SyncVar 이론 지식 : 
            얘는 '서버' 의 변경을 감지함.
            서버에서 이 값에 변화가 생긴다면 이를 일괄적으로 클라이언트들의 값에도 변경을 가함.
            반대로, 클라이언트에서 아무리 이 값을 바꿔도 서버의 값은 변하지 않음.

        Hook 함수 지식 :
            얘는 SyncVar 에 의해 업데이트가 발생했을 때 추가적으로 처리해야 하는 작업을 수행함.
            예를 들면, 서버측에서 Ready 값을 변경한다면 클라이언트들도 이 변경에 따라 UI를 고쳐야 함.
            이걸 Hook 칸에 함수를 등록하면 그 함수를 변경이 있을 때마다 수행해준다!

            단! 주의할 게 있다. 이걸 쓸 땐 값이 '바로' 적용되지 않는다. hook 함수에게 인자를 넘겨주고
            값을 동기화할 것인지에 대한 결정권을 유저에게 넘긴다!
            우린 그 인자를 oldValue, newValue로 받는다. 그 후 실제로 값에도 이를 적용해야 한다.
    */
    [Header("플레이어의 통신 관련 정보를 담고 있는 클래스입니다")]
    [SyncVar] public int ConnectionID;  //  대체 이걸 왜 SyncVar 로 하는건지 전혀 모르겠다!!!!!!!!!
    [SyncVar] public int PlayerIdNumber;
    [SyncVar] public ulong PlayerSteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;
    [SyncVar(hook = nameof(PlayerReadyUpdate))] public bool Ready;
    //[SyncVar(hook = nameof(PlayerRoleUpdate))] public int Role;
    [SyncVar(hook = nameof(HookPlayerRoleUpdate))] public int Role;
    public Camera playerCamera;
    private void HookPlayerRoleUpdate(int oldValue, int newValue)
    {
        // Role 값이 모종의 이유로 서버로부터 변경되었을 때 모든 클라이언트들에게서 호출됨.
        if(isServer)
        {
             this.Role = newValue;
             Debug.Log("HookPlayerRole : isServer : " + oldValue + "-> " + newValue);
        }
        if(isClient)
        {
            Debug.Log("HookPlayerRole : isClient : UpdatePlayerList()");
            LobbyController.Instance.UpdatePlayerList();
        }
    }
    public void ChangeRole(int roleCode)
    {
        if(isOwned) CmdSetPlayerRole(roleCode);        
    }
    [Command]
    private void CmdSetPlayerRole(int roleCode)
    {
        this.HookPlayerRoleUpdate(this.Role, roleCode);
    }

    public bool IsHost
    {
        get { return PlayerIdNumber == 1; }
    }

    private CustomNetworkManager manager;     private CustomNetworkManager Manager
    {
        get
        {
            if(manager != null) return manager;
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); // 이 객체를 게임 씬까지 끌고 갈 거다!
        Debug.Log("권한 부여 상태 : " + isOwned);
        
    }

    private void Update()
    {
        
        if (isOwned && (this.CompareTag("Blind") || this.CompareTag("Limb")))
        {
            if (this.CompareTag("Limb"))
            {
                if (GameManager.instance.Ldcontainer.isRiding)
                {
                    Camera.main.transform.position = GameManager.instance.Pdcontainer.position + new Vector3(0, 0, -10);
                }
                else
                {
                    Camera.main.transform.position = this.transform.position + new Vector3(0, 0, -10);
                }
            }
            else
            {
                Camera.main.transform.position = this.transform.position + new Vector3(0, 0, -10);
            }
        }
    }
    public void CanStartGame(string SceneName)
    {
        if (isOwned)
        {
            CmdCanStartGame(SceneName);
        }
        
    }
    [Command] public void CmdCanStartGame(string SceneName)
    {
        manager.StartGame(SceneName);
    }

#region 레디 상태 변경 영역
    private void PlayerReadyUpdate(bool oldValue, bool newValue)
    {
        if(isServer) this.Ready = newValue;
        if(isClient) LobbyController.Instance.UpdatePlayerList();
    }
    [Command]
    private void CmdSetPlayerReady() 
    {
        this.PlayerReadyUpdate(this.Ready, !this.Ready);
    }
    public void ChangeReady()
    {
        if(isOwned) CmdSetPlayerReady();
    }
#endregion

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLocalPlayer(this);
        LobbyController.Instance.UpdateLobbyID();
    }

    public override void OnStartClient()
    {
        Manager.GamePlayers.Add(this);
        LobbyController.Instance.UpdateLobbyID();
        LobbyController.Instance.UpdatePlayerList();
    }

    public override void OnStopClient()
    {
        Manager.GamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }

    [Command]
    private void CmdSetPlayerName(string playerName)
    {
        this.PlayerNameUpdate(this.PlayerName, playerName);
    }

    public void PlayerNameUpdate(string OldValue, string NewValue)
    {
        if(isServer)
        {
            this.PlayerName = NewValue;
        }
        if(isClient)
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    [ClientRpc]
    public void RpcSetActive(bool newvalue)
    {
        this.gameObject.SetActive(newvalue);
    }

}
