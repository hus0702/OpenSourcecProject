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
            if (spriteForBlind == null) Debug.LogError(gameObject.name + " ��ü�� ����ε����� ������ �� ��������Ʈ�� �ٿ��ּ���!");
            else GetComponent<SpriteRenderer>().sprite = spriteForBlind;
        }
        TODO �ӽ� �ּ�
        */
    }
}
