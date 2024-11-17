using System.Collections.Generic;
using System.Data;
using Mirror;
using Steamworks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField] private PlayerObjectController GamePlayerPrefab;
    public PlayerObjectController GamePlayerInstance;
    public List<PlayerObjectController> GamePlayers { get; } = new List<PlayerObjectController>();
    public ulong CurrentLobbyID {get; private set;}

    GameObject additionalInstance;
    public void SetCurrentLobbyID(ulong id){
        this.CurrentLobbyID = id;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Debug.Log("OnServerAddPlayer");
        //base.OnServerAddPlayer(conn);
        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            GamePlayerInstance = Instantiate(GamePlayerPrefab);
            GamePlayerInstance.ConnectionID = conn.connectionId;
            GamePlayerInstance.PlayerIdNumber = GamePlayers.Count + 1;
            GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);
        
            NetworkServer.AddPlayerForConnection(conn, GamePlayerInstance.gameObject);
        }
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        
        // 게임 씬에 진입했을 때 프리팹을 추가로 생성하도록 처리
        if (sceneName == "GameScene") // "GameScene"을 실제 게임 씬 이름으로 변경
        {
            Debug.Log("GameScene 입성");
            foreach (NetworkConnectionToClient conn in NetworkServer.connections.Values)
            {
                // 추가 프리팹 생성 위치 지정 (여기서는 간단히 랜덤 위치 사용)
                PlayerObjectController playerObjectController = conn.identity.gameObject.GetComponent<PlayerObjectController>();
                
                if (playerObjectController.Role == 1)
                {
                    additionalInstance = Instantiate(spawnPrefabs[0], conn.identity.transform.position + new Vector3(0,2,0), Quaternion.identity);
                }
                else if (playerObjectController.Role == 2)
                {
                    additionalInstance = Instantiate(spawnPrefabs[1], conn.identity.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                }
                // 네트워크에 추가 프리팹을 생성
                NetworkServer.Spawn(additionalInstance, conn);

            }
        }
    }
    public void StartGame(string SceneName)
    {
        ServerChangeScene(SceneName);
    }
}
