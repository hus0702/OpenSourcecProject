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
        SpawnPositionOnLoad = new Vector3(-22.5f, 1, 0);
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
