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
            Debug.Log("�κ񿡼� ������ connection : " + conn);
        }
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        
        // ���� ���� �������� �� �������� �߰��� �����ϵ��� ó��
        if (sceneName == "GameScene") // "GameScene"�� ���� ���� �� �̸����� ����
        {
            if (GameManager.instance == null) // GameManager �̱����� �̹� �ִ��� Ȯ��
            {
                // GameManager �������� �߰� (���⼭�� Resources �������� �ҷ����� ��� ���)
                GameObject gameManagerPrefab = spawnPrefabs[2];
                if (gameManagerPrefab != null)
                {
                    GameObject gameManagerInstance = Instantiate(gameManagerPrefab);
                    NetworkServer.Spawn(gameManagerInstance); // ��Ʈ��ũ �󿡼� GameManager ��ȯ
                    Debug.Log("GameManager�� �����Ǿ����ϴ�.");
                }
                else
                {
                    Debug.LogError("GameManager �������� ã�� �� �����ϴ�. Resources ������ �������� �߰��ϼ���.");
                }
            }
            Debug.Log("GameScene �Լ�");
            foreach (var conn in NetworkServer.connections.Values)
            {
                // �߰� ������ ���� ��ġ ���� (���⼭�� ������ ���� ��ġ ���)
                PlayerObjectController playerObjectController = conn.identity.gameObject.GetComponent<PlayerObjectController>();

                Debug.Log("���Ӿ����� ������ connection : " + conn);
                if (playerObjectController.Role == 1)
                {
                    GamePlayerInstance = Instantiate(spawnPrefabs[0].GetComponent<PlayerObjectController>());
                    GamePlayerInstance.ConnectionID = conn.connectionId;
                    GamePlayerInstance.PlayerIdNumber = GamePlayers.Count + 1;
                    GamePlayerInstance.Role = 1;
                    GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);
                    Debug.Log("��� ����");
                }
                else if (playerObjectController.Role == 2)
                {
                    GamePlayerInstance = Instantiate(spawnPrefabs[1].GetComponent<PlayerObjectController>());
                    GamePlayerInstance.ConnectionID = conn.connectionId;
                    GamePlayerInstance.PlayerIdNumber = GamePlayers.Count + 1;
                    GamePlayerInstance.Role = 2;
                    GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);
                }

                NetworkServer.Spawn(GamePlayerInstance.gameObject, conn);
            }
        }
    }

    public void StartGame(string SceneName)
    {
        ServerChangeScene(SceneName);
    }
}
