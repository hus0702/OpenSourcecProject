using UnityEngine;

/*
    TIO 는 Trigger Interact On ColliderOverlap 의 줄임말입니다
*/
public class TIO_onetime : TriggerInteractOnColliderOverlap
{
    public override void Interact(GameObject requester)
    {
        base.Interact(requester);
        this.myCollider.enabled = false;
    }

    public void Reactive()
    {
        this.myCollider.enabled = true;
    }
}
