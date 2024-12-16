using UnityEngine;
using Mirror;

public class BlindRenderer : MonoBehaviour
{
    [SerializeField] private Sprite spriteForBlind;
    [SerializeField] private RuntimeAnimatorController animatorForBlind;

    protected virtual void Awake()
    {
        PlayerObjectController localPlayer = NetworkClient.localPlayer.GetComponent<PlayerObjectController>();

        if(localPlayer.Role == PlayerObjectController.Blind)
        {
            if (spriteForBlind == null) Debug.LogError(gameObject.name + "BlindRender 객체에 블라인드용 스프라이트가 할당되지 않았습니다!");
            else GetComponent<SpriteRenderer>().sprite = spriteForBlind;


            if (animatorForBlind != null)
            {
                GetComponent<Animator>().runtimeAnimatorController = animatorForBlind;
            }

        }
    }
}
