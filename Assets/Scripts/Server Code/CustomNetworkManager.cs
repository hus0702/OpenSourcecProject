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
            Debug.Log("로비에서 생성된 connection : " + conn);
        }
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        
        // 게임 씬에 진입했을 때 프리팹을 추가로 생성하도록 처리
        if (sceneName == "GameScene") // "GameScene"을 실제 게임 씬 이름으로 변경
        {
            if (GameManager.instance == null) // GameManager 싱글톤이 이미 있는지 확인
            {
                // GameManager 프리팹을 추가 (여기서는 Resources 폴더에서 불러오는 방식 사용)
                GameObject gameManagerPrefab = spawnPrefabs[2];
                if (gameManagerPrefab != null)
                {
                    GameObject gameManagerInstance = Instantiate(gameManagerPrefab);
                    NetworkServer.Spawn(gameManagerInstance); // 네트워크 상에서 GameManager 소환
                    Debug.Log("GameManager가 생성되었습니다.");
                }
                else
                {
                    Debug.LogError("GameManager 프리팹을 찾을 수 없습니다. Resources 폴더에 프리팹을 추가하세요.");
                }
            }
            Debug.Log("GameScene 입성");
            foreach (var conn in NetworkServer.connections.Values)
            {
                // 추가 프리팹 생성 위치 지정 (여기서는 간단히 랜덤 위치 사용)
                PlayerObjectController playerObjectController = conn.identity.gameObject.GetComponent<PlayerObjectController>();
                NetworkServer.Destroy(conn.identity.gameObject);
                if (playerObjectController.Role == 1)
                {
                    GamePlayerInstance = Instantiate(spawnPrefabs[0].GetComponent<PlayerObjectController>(), spawnPrefabs[0].transform.position + new Vector3(0, 1, 0), spawnPrefabs[0].transform.rotation);
                    GamePlayerInstance.ConnectionID = playerObjectController.ConnectionID;
                    GamePlayerInstance.PlayerIdNumber = playerObjectController.PlayerIdNumber;
                    GamePlayerInstance.PlayerSteamID = playerObjectController.PlayerSteamID;
                    GamePlayerInstance.PlayerName = playerObjectController.PlayerName;
                    GamePlayerInstance.Ready = true;
                    GamePlayerInstance.Role = 1;
                }
                else if (playerObjectController.Role == 2)
                {
                    GamePlayerInstance = Instantiate(spawnPrefabs[1].GetComponent<PlayerObjectController>(), spawnPrefabs[1].transform.position + new Vector3(0, 1, 0), spawnPrefabs[1].transform.rotation);
                    GamePlayerInstance.ConnectionID = playerObjectController.ConnectionID;
                    GamePlayerInstance.PlayerIdNumber = playerObjectController.PlayerIdNumber;
                    GamePlayerInstance.PlayerSteamID = playerObjectController.PlayerSteamID;
                    GamePlayerInstance.PlayerName = playerObjectController.PlayerName;
                    GamePlayerInstance.Ready = true;
                    GamePlayerInstance.Role = 2;
                }
                NetworkServer.Spawn(GamePlayerInstance.gameObject,conn);
            }
        }
    }

    public void StartGame(string SceneName)
    {
        ServerChangeScene(SceneName);
    }
}
