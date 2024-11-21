using System.Collections;
using Mirror;
using UnityEngine;

public class CardSlot : InteractableObject
{
    [SerializeField] private string keyName;
    [SyncVar(hook = nameof(HookIsKeyInserted))] public bool IsKeyInserted = false;
    private void HookIsKeyInserted(bool OldValue, bool NewValue)
    {
        Debug.Log("IsKeyInserted 가 " + NewValue + "로 변경됐습니다.");
    }

    public override bool CheckInteractable(GameObject requester)
    {
        if(IsKeyInserted) return false;
        // 만약 플레이어가 지정된 카드키를 지니고 있는 상태라면
        // Player = requester.GetComponent; if (Player.hasItem(keyName))

        return true;

        // else return false;
    }
    public override void ExecuteOnSuccess(GameObject requester)
    {
        // 상호작용에 성공했을 경우
        CmdSetKeyInserted();
    }
    [Command] private void CmdSetKeyInserted()=>IsKeyInserted = !IsKeyInserted;

    public void OnTriggerExit2D(Collider2D outer){ // 상호작용한다고 했는데 그 자리를 벗어나면 키 삽입 상태가 취소된다!
        //if (!isServer) return;
        Debug.Log("카드슬롯에 대해 OnTriggerExit 이 발동! isServer : " + isServer);
        //if(outer.CompareTag("Player"))
        /*if(IsKeyInserted)*/
        CmdSetKeyInserted();
        IsKeyInserted = !IsKeyInserted;
        
    }

    public override void ExecuteOnFail(GameObject requester)
    {
        if(IsKeyInserted) return; // 열쇠가 꽂혀 있는 상태면 실패 메시지를 따로 출력하지 않는다.

        Debug.Log(keyName + "이(가) 필요한 것 같다.");
        // 나중에 UI로 띄울 것.
    }

    //TODO 이렇게 말고 따로 인터페이스를 띄우는 게 더 이뻐보이긴 함. 이건 맨 나중에 하자.
}
