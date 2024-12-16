using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : NetworkBehaviour, ITriggered
{
    public string sceneName;
    public Vector3 spawnPointOnLoadForBlind;
    public Vector3 spawnPointOnLoadForLimp;

    private CustomNetworkManager manager;
    private CustomNetworkManager Manager
    {
        get
        {
            if(manager != null) return manager;
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void BeforeStartScene()
    {
        SetPlayerSpawnPositionOnLoad();
    }

    private void SetPlayerSpawnPositionOnLoad()
    {
        /*
            민규님 요청사항 반영
        */
        spawnPointOnLoadForBlind.z = -1;
        spawnPointOnLoadForLimp.z = -1;
        Debug.Log("SceneChanger : SpawnPoint의 z 축 값을 -1로 변경했습니다.");

        GameManager.instance.BlindSpawnPositionOnLoad = spawnPointOnLoadForBlind;
        GameManager.instance.LimpSpawnPositionOnLoad = spawnPointOnLoadForLimp;

        Debug.Log("블라인드 스폰 포인트 : " + GameManager.instance.BlindSpawnPositionOnLoad.x + " , " + GameManager.instance.BlindSpawnPositionOnLoad.y);
        Debug.Log("절름발이 스폰 포인트 : " + GameManager.instance.LimpSpawnPositionOnLoad.x + " , " + GameManager.instance.LimpSpawnPositionOnLoad.y);
    }

    public void StartScene()
    {
        BeforeStartScene();

        var localPlayer = NetworkClient.localPlayer.GetComponent<NetworkIdentity>();
        
        RPCUpdatePlayerActivation(sceneName);

        Debug.Log("씬을 전환합니다!");

        if(localPlayer.isServer) Manager.StartGame(sceneName);
        
        //SceneManager.LoadScene(sceneName);
    }

    [ClientRpc]
    private void RPCUpdatePlayerActivation(string sceneName)
    {
        // "클라이언트 측" 에서 호출되는 함수임.

        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            if(sceneName == "GameScene")
            {
                player.gameObject.SetActive(true);
            }
            else
            {
                player.gameObject.SetActive(false);
            }

            var playerPlayer = player.GetComponent<Player>();
            if(playerPlayer != null)
            {
                Debug.Log("Player 컴포넌트가 붙은 플레이어를 발견, 상호작용 가능 여부를 활성화합니다.");
                playerPlayer.isInteractable = true;
            }
        }
    }

    public void Trigger()
    {
        StartScene();
    }
}
