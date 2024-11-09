using UnityEngine;
using Mirror;

public class Cabinet : InteractableObject
{
    private Animator myAnimator;
    protected override void Awake()
    {
        base.Awake();
        myAnimator = GetComponent<Animator>();
    }
    public override bool CheckInteractable(GameObject requester)
    {
        return isColliderOverlap(requester.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public override void ExecuteOnSuccess(GameObject requester)
    {
        base.ExecuteOnSuccess(requester);
        // 캐비넷에서 해줄게 뭔가 있을까요....?
        // => 있죠?
        myAnimator.SetBool("isInteracted", true);
    }
    
    public void OnFinishAnimation()=>myAnimator.SetBool("isInteracted", false);
}
