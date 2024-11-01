using Mirror;
using UnityEngine;

public class GameSceneControllerDummy : MonoBehaviour
{
    private PlayeDummyManager localPlayerManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        localPlayerManager = NetworkClient.localPlayer.GetComponent<PlayeDummyManager>();
    }

    public void UpdateHakJeom()
    {
        localPlayerManager.ChangeHakJeom();
    }

    public void UpdateTaskContidion()
    {
        localPlayerManager.ChangeTaskContidion();
    }
}
