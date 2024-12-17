using UnityEngine;

public class DoorNeedTogether : Door
{
    public override bool CheckInteractable(GameObject requester)
    {
        // 만약 두 명이 아니라면
        if(GameManager.instance.Pdcontainer.iscarrying) return false;
        //else return base.CheckInteractable(requester);
        else return base.CheckInteractable(requester);
    }
}
