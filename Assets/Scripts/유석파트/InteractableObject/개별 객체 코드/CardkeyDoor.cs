using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardkeyDoor : Door
{
    [SerializeField] private List<DoorCardSlot> cardSlots;
    [SyncVar(hook = nameof(HookIsOpened))] public bool isAnimatedOpened = false;
    private void HookIsOpened(bool oldVaule, bool newValue)
    {
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

        isAnimatedOpened = true;

        //CmdOpenDoor();
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


    [Command] private void CmdOpenDoor()=>isOpened = true;
}
