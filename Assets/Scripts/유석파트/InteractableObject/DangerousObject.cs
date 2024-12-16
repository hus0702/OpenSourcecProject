using UnityEngine;
using Mirror;

public class DangerousObject : ColliderOverlapInteractable
{
    //[SyncVar(hook = nameof(OnInActiveObject))]private bool isActive = true;
    private void OnInActiveObject(bool oldValue, bool newValue)
    {
        // 꺼졌을 때 애니메이션을 작동하면 되겠습니다
        Debug.Log("위험한 객체가 비활성화됐습니다. : " + gameObject.name);
    }

    public override bool CheckInteractable(GameObject requester)
    {
        //return isActive;
        return true;
    }

    public override void ExecuteOnSuccess(GameObject requester)
    {
        OnKillPlayer(requester);
    }

    public virtual void OnKillPlayer(GameObject requester)
    {
        Debug.Log("플레이어를 죽이겠습니다.");
        // 플레이어를 죽일 고유 애니메이션을 재생한다던가 그런 로직을 추가하면 됨.
        //Player.Die();

        /////////////////////////////////////////////////// 송민규 팀원이 코드 변경했습니다.

        if (requester.tag == "Blind")
        {
            requester.GetComponent<Player>().TakingDamage(10); // 바로 죽임
        }
        else if (requester.tag == "Limb")
        {
            requester.GetComponent<Limb>().TakingDamage(10); // 바로 죽임
        }
        
        /////////////////////////////////////////////////// 이 사이 부분을 변경했습니다.
    }

    public virtual void OnKeyInserted()
    {
        //InActiveObject();
    }

    //[Command(requiresAuthority =false)]public void InActiveObject()=>isActive=false;
}
