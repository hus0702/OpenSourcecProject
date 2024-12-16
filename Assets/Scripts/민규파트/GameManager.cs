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
        Instantiate(AudioManager);
    }

    private void Update()
    {
        if ((Pdcontainer.Respawncall || Ldcontainer.Respawncall)&&isServer)
        {
            Respawn();
        }
    }
    private void Start()
    {
        BlindSpawnPositionOnLoad = new Vector3(-22.5f, 1, -1);
        LimpSpawnPositionOnLoad = new Vector3(-22.5f, 1, -1);


        //BlindSpawnPositionOnLoad = new Vector3(1f, 1, -1);
        //LimpSpawnPositionOnLoad = new Vector3(1f, 1, -1);

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
            Debug.Log("blind ������Ʈ�� GameManager�� �����ϴ�.");
        }
        if (Limp != null)
        {
            Limp.GetComponent<Limb>().Respawn();
        }
        else
        {
            Debug.Log("Limp ������Ʈ�� GameManager�� �����ϴ�.");
        }

        Pdcontainer.Respawncall = false;
        Ldcontainer.Respawncall = false;
    }

    // �������� Ŭ���̾�Ʈ�� ȣ��Ǵ� RPC
    [ClientRpc]
    public void RpcPlaySoundOnClient(AudioManager.Sfx name)
    {
        AudioManager.instance.PlaySfx(name);
    }

    public void PlaySoundOnClient(AudioManager.Sfx name)
    {
        AudioManager.instance.PlaySfx(name);
    }

}
