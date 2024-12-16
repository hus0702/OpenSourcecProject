using UnityEngine;

public class DoorCardSlot : CardSlot
{
    [SerializeField] private CardkeyDoor doorToOpen;

    public override void ExecuteOnSuccess(GameObject requester)
    {
        base.ExecuteOnSuccess(requester);

        doorToOpen.CheckAllKeyInputed();
    }

    public void OnDoorOpened()=>InActiveInteract();
}