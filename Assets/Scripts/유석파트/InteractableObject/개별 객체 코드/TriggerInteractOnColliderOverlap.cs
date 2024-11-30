using UnityEngine;

public class TriggerInteractOnColliderOverlap : ColliderOverlapInteractable
{
    public Trigger trigger;

    public override void ExecuteOnSuccess(GameObject requester)
    {
        trigger.ActiveTrigger();
    }
}
