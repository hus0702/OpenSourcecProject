using UnityEngine;
using Mirror;
using Steamworks;
using System;
using System.Data;

/*
    필요할 만 한 기능 : 
    1. 상호작용이 가능한지 우선 체크한다.
        
        만약 가능하다면...
        2. 요청자의 Interact 함수를 호출하면서, 자신을 인자로 준다.
                이걸 통해 플레이어도 어떤 물체에 대해 상호작용하는 것을 성공했는지 확인 가능.
        3. 자기는 자기 할거 한다.

        만약 상호작용에 실패했다면...
        4. 요청자의 Interact 함수를 호출하지 않는다! 그냥 그러면 됨
        5. 자기는 상호작용에 실패했을 때 수행할 함수를 호출한다. 
*/
public class InteractableObject : NetworkBehaviour, IInteracted
{
    protected Collider2D myCollider;

    protected virtual void Awake(){
        myCollider = GetComponent<Collider2D>();
    }

    public virtual void Interact(GameObject requester) //TODO 이 부분은 나중에 바뀔 수 있음.
    {
        Debug.Log(gameObject.name + " 객체와 상호작용합니다.");

        bool isInteractable = CheckInteractable(requester); // 우선 상호작용이 가능한지부터 체크한다.

        if(isInteractable) ExecuteOnSuccess(requester);
        else ExecuteOnFail(requester);
    }

    public virtual bool CheckInteractable(GameObject requester)
    {
        return true; // 디폴트는 언제나 가능한 상태.
    }

    public virtual void ExecuteOnSuccess(GameObject requester){
        Debug.Log(gameObject.name + "객체와 상호작용에 성공했습니다.");
    }

    public virtual void ExecuteOnFail(GameObject requester){}

    protected bool isColliderOverlap(Collider2D arg1, Collider2D arg2)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(arg1.bounds.center, arg1.bounds.size, 0f);

        foreach(var collider in colliders)
        {
            if(collider == arg2) return true;
        }
        return false;
    }

    public void ActiveInteract()=>myCollider.enabled = true;
    public void InActiveInteract()=>myCollider.enabled = false;
}