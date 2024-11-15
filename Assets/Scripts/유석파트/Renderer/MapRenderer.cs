using UnityEngine;
using Mirror;

public class MapRenderer : MonoBehaviour
{
    [SerializeField] private GameObject blindMap;
    [SerializeField] private GameObject limpMap;
    void Awake()
    {
        PlayerObjectController localPlayer = NetworkClient.localPlayer.GetComponent<PlayerObjectController>();

        if(localPlayer.Role == PlayerObjectController.Blind)
        {
            blindMap.SetActive(true);
            limpMap.SetActive(false);
        }
        else
        {
            blindMap.SetActive(false);
            limpMap.SetActive(true);
        }
    }
}
