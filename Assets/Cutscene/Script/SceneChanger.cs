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
    }

    public void StartScene()
    {
        BeforeStartScene();

        Manager.StartGame(sceneName);
        
        //SceneManager.LoadScene(sceneName);
    }

    public void Trigger()
    {
        StartScene();
    }
}
