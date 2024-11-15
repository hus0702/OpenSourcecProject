using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardkeyDoor : Door
{
    [SerializeField] private List<DoorCardSlot> cardSlots;
    [SyncVar(hook = nameof(HookIsOpened))] public bool isOpened = false;
    private void HookIsOpened(bool oldVaule, bool newValue)
    {
        if(newValue == true) ActiveInteract();
        else InActiveInteract();
    }

    protected override void Awake()
    {
        base.Awake();
        // if ( 씬이 로드됐는데 열려있지 않아야 한다면 ) //
        InActiveInteract();
    }

    public void CheckAllKeyInputed(){
        foreach(CardSlot cardSlot in cardSlots)
        {
            if(!cardSlot.IsKeyInserted) return;
        }
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

        CmdOpenDoor();
    }
    [Command] private void CmdOpenDoor()=>isOpened = true;
}
