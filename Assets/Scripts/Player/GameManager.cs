using Mirror;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public PlayerDataContainer Pdcontainer;
    public LimbDataContainer Ldcontainer;

    private void Awake()
    {
        // 일반적인 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("나는 서버야");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("클라이언트인데유");
    }
}
