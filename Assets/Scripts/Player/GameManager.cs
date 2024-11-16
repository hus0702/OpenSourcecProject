using Mirror;
using Mirror.BouncyCastle.Bcpg;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public PlayerData PlayerData;
    public LimbData LimbData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


}
