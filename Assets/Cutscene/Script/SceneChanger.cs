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



        if(sceneName == "GameScene")
        {
            foreach(PlayerObjectController player in Manager.GamePlayers)
            {
                player.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(PlayerObjectController player in Manager.GamePlayers)
            {
                player.gameObject.SetActive(false);
            }
        }


    }

    private void SetPlayerSpawnPositionOnLoad()
    {        
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
