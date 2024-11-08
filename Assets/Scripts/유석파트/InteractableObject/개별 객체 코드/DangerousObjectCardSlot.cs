using Unity.VisualScripting;
using UnityEngine;

public class DangerousObjectCardSlot : CardSlot
{
    [SerializeField] private DangerousObject dangerousObjectToInActive;
    
    public override void ExecuteOnSuccess(GameObject requester)
    {
        base.ExecuteOnSuccess(requester);

        dangerousObjectToInActive.OnKeyInserted();

        InActiveInteract();
    }
}
