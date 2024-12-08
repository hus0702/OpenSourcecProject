using Mirror;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public PlayerDataContainer Pdcontainer;
    public LimbDataContainer Ldcontainer;

    public Vector3 BlindSpawnPositionOnLoad;
    public Vector3 LimpSpawnPositionOnLoad;

    public bool isGameStarted;
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
        BlindSpawnPositionOnLoad = new Vector3(-22.5f, 1, 0);
        LimpSpawnPositionOnLoad = new Vector3(-22.5f, 1, 0);
        isGameStarted = false;
    }

    [Command]
    public void CmdPlaySoundOnClient(AudioManager.Sfx name)
    {
        AudioManager.instance.PlaySfx(name);
        RpcPlaySoundOnClient(name);
    }

    // 서버에서 클라이언트로 호출되는 RPC
    [ClientRpc]
    void RpcPlaySoundOnClient(AudioManager.Sfx name)
    {
        AudioManager.instance.PlaySfx(name);
    }

}
