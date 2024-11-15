using Mirror;
using Mirror.BouncyCastle.Bcpg;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerData PlayerData;
    public PlayerDataContainer PlayerDataContainer;
    public LimbData LimbData;
    public LimbDataContainer LimbDataContainer;

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
