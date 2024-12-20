using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardkeyDoor : Door
{
    [SerializeField] private List<DoorCardSlot> cardSlots;
    [SyncVar(hook = nameof(HookIsOpened))] public bool isAnimatedOpened = false;
    private void HookIsOpened(bool oldVaule, bool newValue)
    {
        Debug.Log("문이 열리는 애니메이션을 작동");
        if(newValue == true)
        {
            Animator doorAnimator = GetComponent<Animator>();
            doorAnimator.SetBool("isIdle", false);
            doorAnimator.SetBool("isOpened", true);
        }

        isOpened = true;
    }
    public GameObject errorMessagePosition;

    public void CheckAllKeyInputed(){
        bool isAllInserted = true;
        foreach(CardSlot cardSlot in cardSlots)
        {
            if(!cardSlot.IsKeyInserted)
            {
                Debug.Log("카드 슬롯 " + cardSlot.gameObject.name + "이 아직 삽입이 안 됨.");
                isAllInserted = false;
            }
        }
        if(!isAllInserted) return;
        // 모든 키가 동시에 입력되어 있는 것을 확인했다!
        OnEveryKeyInserted(); // 문 열리는 이벤트를 실행한다.
    }

    public void OnEveryKeyInserted()
    {
        foreach(DoorCardSlot cardSlot in cardSlots)
        {
            cardSlot.OnDoorOpened();
        }
        Debug.Log("문이 열렸습니다!"); // 나중에 문 열리는 이벤트로 대체할 것.

        SWM.Instance.MakeSoundwave((int)AudioManager.Sfx.opendoor, true, gameObject, 4f, 0.8f);

        CmdSetIsAnimatedOpened(true);
        //CmdOpenDoor();
    }

    [Command(requiresAuthority = false)] public void CmdSetIsAnimatedOpened(bool val)
    {
        isAnimatedOpened = val;
    }

    public override void ExecuteOnFail(GameObject requester)
    {

        Debug.Log("문이 잠겨 있습니다.");
        // 실제로는 UI 로 말풍선 비슷하게 띄울 것. 필요한 아이템 이미지가 말풍선 안에 들어가고, 진동하게 만들 것.
        if(failHandler != null)
        {
            failHandler.FailHandle(errorMessagePosition, failHandleInfo);
        }
    }

    public override void ExecuteOnSuccess(GameObject requester)
    {
        // 아무 소리를 내지 않고 플레이어의 Transform 을 텔레포트시켜야 함.
        TransportRequester(requester);
    }


    [Command] private void CmdOpenDoor()=>isOpened = true;
}
