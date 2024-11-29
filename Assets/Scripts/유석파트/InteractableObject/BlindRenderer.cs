using UnityEngine;
using Mirror;

public class BlindRenderer : MonoBehaviour
{
    [SerializeField] private Sprite spriteForBlind;

    void Awake()
    {
        /*
        PlayerObjectController localPlayer = NetworkClient.localPlayer.GetComponent<PlayerObjectController>();

        if(localPlayer.Role == PlayerObjectController.Blind)
        {
            if (spriteForBlind == null) Debug.LogError(gameObject.name + " 객체에 블라인드한테 렌더링 될 스프라이트를 붙여주세요!");
            else GetComponent<SpriteRenderer>().sprite = spriteForBlind;
        }
        TODO 임시 주석
        */
    }
}
