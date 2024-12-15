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
            Debug.Log("�κ񿡼� ������ connection : " + conn);

            if (GameManager.instance == null) // GameManager �̱����� �̹� �ִ��� Ȯ��
            {
                // GameManager �������� �߰� (���⼭�� Resources �������� �ҷ����� ��� ���)
                GameObject gameManagerPrefab = spawnPrefabs[2];
                if (gameManagerPrefab != null)
                {
                    GameObject gameManagerInstance = Instantiate(gameManagerPrefab);
                    NetworkServer.Spawn(gameManagerInstance, conn); // ��Ʈ��ũ �󿡼� GameManager ��ȯ
                    Debug.Log("GameManager�� �����Ǿ����ϴ�.");
                }
                else
                {
                    Debug.LogError("GameManager �������� ã�� �� �����ϴ�. Resources ������ �������� �߰��ϼ���.");
                }
            }
        }
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        // ���� ���� �������� �� �������� �߰��� �����ϵ��� ó��
        if (sceneName == "GameScene") // "GameScene"�� ���� ���� �� �̸����� ����
        {
            if (!GameManager.instance.isGameStarted)
            {
                GameManager.instance.isGameStarted = true;
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
                        conn.gameObject.transform.position = GameManager.instance.LimpSpawnPositionOnLoad;
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


