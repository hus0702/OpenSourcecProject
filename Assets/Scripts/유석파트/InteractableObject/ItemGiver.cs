using UnityEngine;
using Mirror;

public class ItemGiver : InteractableObject
{
    [SyncVar]private bool isHaveItem = true;
    public GameObject itemToGive;

    public override bool CheckInteractable(GameObject requester)
    {
        if(isHaveItem)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void ExecuteOnSuccess(GameObject requester)
    {
        // 플레이어한테 아이템을 전달해주면 됩니다.
    }
}
