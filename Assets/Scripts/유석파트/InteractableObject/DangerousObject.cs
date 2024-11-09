using UnityEngine;
using Mirror;

public class DangerousObject : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnInActiveObject))]private bool isActive = true;
    private void OnInActiveObject(bool oldValue, bool newValue)
    {
        // 꺼졌을 때 애니메이션을 작동하면 되겠습니다
        Debug.Log("위험한 객체가 비활성화됐습니다. : " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(isActive)
        {
            // 만약 닿은게 플레이어라면
            // 플레이어를 죽인다
            OnKillPlayer();
        }
    }
    public virtual void OnKillPlayer()
    {
        // 플레이어를 죽일 고유 애니메이션을 재생한다던가 그런 로직을 추가하면 됨.
        //Player.Die();
    }

    public virtual void OnKeyInserted()
    {
        InActiveObject();
    }

    [Command]public void InActiveObject()=>isActive=false;
}
