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
    public GameObject GameScenePrefab;
    public List<PlayerObjectController> GamePlayers { get; } = new List<PlayerObjectController>();
    public ulong CurrentLobbyID { get; private set; }

    GameObject additionalInstance;
    public void SetCurrentLobbyID(ulong id) {
        this.CurrentLobbyID = id;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Debug.Log("OnServerAddPlayer");
        //base.OnServerAddPlayer(conn);
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            GamePlayerInstance = Instantiate(GamePlayerPrefab);
            GamePlayerInstance.ConnectionID = conn.connectionId;
            GamePlayerInstance.PlayerIdNumber = GamePlayers.Count + 1;
            GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);

            NetworkServer.AddPlayerForConnection(conn, GamePlayerInstance.gameObject);
            Debug.Log("로비에서 생성된 connection : " + conn);

            if (GameManager.instance == null) // GameManager 싱글톤이 이미 있는지 확인
            {
                // GameManager 프리팹을 추가 (여기서는 Resources 폴더에서 불러오는 방식 사용)
                GameObject gameManagerPrefab = spawnPrefabs[2];
                if (gameManagerPrefab != null)
                {
                    GameObject gameManagerInstance = Instantiate(gameManagerPrefab);
                    NetworkServer.Spawn(gameManagerInstance, conn); // 네트워크 상에서 GameManager 소환
                    Debug.Log("GameManager가 생성되었습니다.");
                }
                else
                {
                    Debug.LogError("GameManager 프리팹을 찾을 수 없습니다. Resources 폴더에 프리팹을 추가하세요.");
                }
            }
        }
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        // 게임 씬에 진입했을 때 프리팹을 추가로 생성하도록 처리
        if (sceneName == "GameScene") // "GameScene"을 실제 게임 씬 이름으로 변경
        {
            Rigidbody2D playerobject = NetworkClient.localPlayer.GetComponent<Rigidbody2D>();
            if (playerobject != null)
            {
                SpawnPrefabs();
            }
            else 
            {
                foreach (PlayerObjectController conn in GamePlayers)
                {
                    if (conn.Role == 1)
                    {
                        conn.gameObject.transform.position = GameManager.instance.BlindSpawnPositionOnLoad;
                    }
                    else
                    {
                        conn.gameObject.transform.position = GameManager.instance.BlindSpawnPositionOnLoad;
                    }

                }
            }
        }
    }

    public void StartGame(string SceneName)
    {
        ServerChangeScene(SceneName);
    }

    public void SpawnPrefabs()
    {
        foreach (var conn in NetworkServer.connections.Values)
        {
            PlayerObjectController playerObjectController = conn.identity.gameObject.GetComponent<PlayerObjectController>();
            if (playerObjectController.Role == 1)
            {
                GameScenePrefab = Instantiate(spawnPrefabs[0], GameManager.instance.BlindSpawnPositionOnLoad, spawnPrefabs[0].transform.rotation);
                GameScenePrefab.GetComponent<PlayerObjectController>().ConnectionID = playerObjectController.ConnectionID;
                NetworkServer.Spawn(GameScenePrefab, conn);
            }
            else if (playerObjectController.Role == 2)
            {
                GameScenePrefab = Instantiate(spawnPrefabs[1], GameManager.instance.LimpSpawnPositionOnLoad, spawnPrefabs[1].transform.rotation);
                GameScenePrefab.GetComponent<PlayerObjectController>().ConnectionID = playerObjectController.ConnectionID;
                NetworkServer.Spawn(GameScenePrefab, conn);
            }
        }
    }
}


