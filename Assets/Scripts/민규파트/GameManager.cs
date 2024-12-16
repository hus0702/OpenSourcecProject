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
        // ï¿½Ï¹ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ì±ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
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


        BlindSpawnPositionOnLoad = new Vector3(1f, 1, -1);
        LimpSpawnPositionOnLoad = new Vector3(1f, 1, -1);

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
            Debug.Log("blind ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®ï¿½ï¿½ GameManagerï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½.");
        }
        if (Limp != null)
        {
            Limp.GetComponent<Limb>().Respawn();
        }
        else
        {
            Debug.Log("Limp ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®ï¿½ï¿½ GameManagerï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½.");
        }

        Pdcontainer.Respawncall = false;
        Ldcontainer.Respawncall = false;
    }

<<<<<<< Updated upstream
    [Command]
    public void CmdPlaySoundOnClient(AudioManager.Sfx name)
    {
        AudioManager.instance.PlaySfx(name);
        RpcPlaySoundOnClient(name);
    }

    // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ Å¬ï¿½ï¿½ï¿½Ì¾ï¿½Æ®ï¿½ï¿½ È£ï¿½ï¿½Ç´ï¿½ RPC
=======
    // ¼­¹ö¿¡¼­ Å¬¶óÀÌ¾ðÆ®·Î È£ÃâµÇ´Â RPC
>>>>>>> Stashed changes
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
