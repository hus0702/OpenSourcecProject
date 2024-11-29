using UnityEngine;
using Mirror;

public class BlindRenderer : MonoBehaviour
{
    [SerializeField] private Sprite spriteForBlind;

    void Awake()
    {
        PlayerObjectController localPlayer = NetworkClient.localPlayer.GetComponent<PlayerObjectController>();

        if(localPlayer.Role == PlayerObjectController.Blind)
        {
            GetComponent<SpriteRenderer>().sprite = spriteForBlind;
        }
    }
}
