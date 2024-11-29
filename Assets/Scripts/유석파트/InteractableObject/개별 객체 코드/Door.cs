using UnityEngine;
using Mirror;

public class Door : InteractableObject
{
    /*
        문의 경우 카드키에 종속적으로 만들어야 함!

        카드키가 상호작용에 성공한 경우 문의 Collider 를 활성화해야 함.
        그 후 드디어 문에 상호작용하는 것이 가능해짐.
    */
    [SerializeField] private Transform teleportDest;

    public override void ExecuteOnSuccess(GameObject requester)
    {
        // 애니메이션이나 효과음의 재생 등이 이루어질 수도 있고

        // 플레이어의 Transform 을 텔레포트시켜야 함.
        requester.transform.position = new Vector3(teleportDest.position.x, teleportDest.position.y, teleportDest.position.z);
    }
}
