using Mirror;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public PlayerDataContainer Pdcontainer;
    public LimbDataContainer Ldcontainer;

    public Vector3 SpawnPositionOnLoad;

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

    private void Start()
    {
        SpawnPositionOnLoad = new Vector3(-22.5f, 1, 0);
    }
}
