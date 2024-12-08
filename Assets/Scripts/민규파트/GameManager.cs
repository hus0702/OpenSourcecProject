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
        // �Ϲ����� �̱��� ���� ����
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

    // �������� Ŭ���̾�Ʈ�� ȣ��Ǵ� RPC
    [ClientRpc]
    void RpcPlaySoundOnClient(AudioManager.Sfx name)
    {
        AudioManager.instance.PlaySfx(name);
    }

}
