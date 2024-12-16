using UnityEngine;
using Mirror;

public class LimpRenderComponent : MonoBehaviour
{
    /*
        이 컴포넌트를 붙이면 장님한테는 객체가 비활성화됩니다!
    */

    private SpriteRenderer mySpriteRenderer;

    void Awake()
    {
        PlayerObjectController localPlayer = NetworkClient.localPlayer.GetComponent<PlayerObjectController>();
        if(localPlayer.Role == PlayerObjectController.Blind)
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            if(mySpriteRenderer != null)
            {
                Color color = mySpriteRenderer.color;
                color.a = 0f;
                mySpriteRenderer.color = color;
            }
            else /*just*/ gameObject.SetActive(false);
        }
    }
}
