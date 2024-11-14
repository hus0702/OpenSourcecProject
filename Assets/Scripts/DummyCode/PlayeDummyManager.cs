using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayeDummyManager : NetworkBehaviour
{

    public float HakJeom = 1.0f; // 학점은 개인만 알고 있으면 됨.
    public void ChangeHakJeom()
    {
        PlayerObjectController thisController = transform.GetComponent<PlayerObjectController>();
        this.HakJeom++; // 학점을 변경하는 함수.
        
        Debug.Log(thisController.PlayerName + "님의 학점이 1.0 점 올랐습니다!");
    }
    // 주목할 건 이 값이 바뀌었을 때 양 쪽의 클라이언트에서 어떻게 보이는지에 대한 여부! 확인해볼 것.





    [SyncVar(hook = nameof(HookIsTaskFinished))] public bool IsTaskFinished; // 내가 과제를 안 해왔는지는 상대방한테도 중요함.
    public void ChangeTaskContidion()
    {
        if(isOwned) CmdChangeTaskContidion();
        /*
            굳이 이런 조건을 추가하는 이유 : 
                이 함수가 내 플레이어 A 가 아닌, 상대방의 플레이어 B에 대해 호출됐을 땐
                클라이언트가 직접 다른 클라이언트의 값을 변경하려고 한 아주 나쁜 사례임.
                따라서 이 경우는 막아주고, 내 값을 내가 바꾸고 싶다! 근데 동기화가 필요하다
                하는 의미로 if(isOwned) 를 조건으로 붙인다!
        */
    }
    [Command] public void CmdChangeTaskContidion()=>this.IsTaskFinished = !IsTaskFinished;
    public void HookIsTaskFinished(bool oldValue, bool newValue)
    {
        PlayerObjectController thisController = transform.GetComponent<PlayerObjectController>(); // 현재 객체에 매달려 있는 PlayerObjectController 를 일단 받아오겠습니다. 닉네임 값이 필요하니까요!
        /*
            이 형태에서 알 수 있듯이, 서버는 항상 값이 변경됨을 감지해서 Hook 함수를 호출할 땐
            oldVal , newVal 이 두 개를 인자로 넘겨줍니다.
            이걸 통해서 하고 싶은 동작을 처리하면 됩니다!
        */
        if(newValue == true)
        {
            Debug.Log(thisController.PlayerName + "님이 과제를 해왔다고 합니다!");
        }
        else
        {
            Debug.LogError(thisController.PlayerName + "님이 숙제를 빼먹었다고 합니다... ;;");
        }
    }

}
