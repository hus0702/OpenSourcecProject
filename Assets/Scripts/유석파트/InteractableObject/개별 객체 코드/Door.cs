using UnityEngine;
using Mirror;

public class Door : InteractableObject
{
    /*
        문의 경우 카드키에 종속적으로 만들어야 함!

        카드키가 상호작용에 성공한 경우 문의 Collider 를 활성화해야 함.
        그 후 드디어 문에 상호작용하는 것이 가능해짐.



        => 이거 수정해야 할 듯. 카드키가 없을 때 문과 상호작용하면 메시지가 떠야 할 것 같은디?
    */
    [SerializeField] private Transform teleportDest;
    public bool isOpened = false;

    public bool isNormalDoor = false;
    public bool isKeyCardDoor = false; // 이 부분을 조작해서 문의 타입을 결정하면 되겠습니다!

    public InteractFailTalkBubble failHandler;
    public SO_FailHandleInfo failHandleInfo;

    public override bool CheckInteractable(GameObject requester)
    {
        if(!isOpened) return false;
        else return true;
    }

    public override void ExecuteOnSuccess(GameObject requester)
    {
            // 애니메이션이나 효과음의 재생 등이 이루어질 수도 있고

            // 플레이어의 Transform 을 텔레포트시켜야 함.
            requester.transform.position = new Vector3(teleportDest.position.x, teleportDest.position.y, teleportDest.position.z);
    }

    public override void ExecuteOnFail(GameObject requester)
    {

        Debug.Log("문이 잠겨 있습니다.");
        // 실제로는 UI 로 말풍선 비슷하게 띄울 것. 필요한 아이템 이미지가 말풍선 안에 들어가고, 진동하게 만들 것.
        if(failHandler != null)
        {
            failHandler.FailHandle(this.gameObject, failHandleInfo);
        }
    }
}
