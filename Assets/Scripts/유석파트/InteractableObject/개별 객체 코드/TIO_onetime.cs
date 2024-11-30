using UnityEngine;

public class TIO_onetime : TriggerInteractOnColliderOverlap
{
    public override void Interact(GameObject requester)
    {
        base.Interact(requester);
        this.myCollider.enabled = false;
    }
}
