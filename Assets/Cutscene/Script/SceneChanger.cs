using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour, ITriggered
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

    public void StartScene()
    {
        PlayerObjectController player = NetworkClient.localPlayer.GetComponent<PlayerObjectController>();

        if(player == null) {Debug.LogError("현재 할당된 로컬 플레이어가 존재하지 않습니다."); return;}
        
        if(player.Role == PlayerObjectController.Blind) GameManager.instance.SpawnPositionOnLoad = spawnPointOnLoadForBlind;
        else GameManager.instance.SpawnPositionOnLoad = spawnPointOnLoadForLimp;

        Manager.StartGame(sceneName);
        
        //SceneManager.LoadScene(sceneName);
    }

    public void Trigger()
    {
        StartScene();
    }
}
