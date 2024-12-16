using Mirror;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public PlayerDataContainer Pdcontainer;
    public LimbDataContainer Ldcontainer;

    public Vector3 BlindSpawnPositionOnLoad;
    public Vector3 LimpSpawnPositionOnLoad;

    public GameObject Blind;
    public GameObject Limp;

    public AudioManager AudioManager;
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
        Instantiate(AudioManager);
    }

    private void Start()
    {
        BlindSpawnPositionOnLoad = new Vector3(-22.5f, 1, -1);
        LimpSpawnPositionOnLoad = new Vector3(-22.5f, 1, -1);
        isGameStarted = false;
    }

    public void Respawn()
    {
        if (Blind != null)
        {
            Blind.GetComponent<Player>().Respawn();
        }
        else
        {
            Debug.Log("blind 오브젝트가 GameManager에 없습니다.");
        }
        if (Limp != null)
        {
            Limp.GetComponent<Limb>().Respawn();
        }
        else
        {
            Debug.Log("Limp 오브젝트가 GameManager에 없습니다.");
        }
        
    }

    [Command]
    public void CmdPlaySoundOnClient(AudioManager.Sfx name)
    {
        AudioManager.instance.PlaySfx(name);
        RpcPlaySoundOnClient(name);
    }

    // 서버에서 클라이언트로 호출되는 RPC
    [ClientRpc]
    public void RpcPlaySoundOnClient(AudioManager.Sfx name)
    {
        AudioManager.instance.PlaySfx(name);
    }

}
