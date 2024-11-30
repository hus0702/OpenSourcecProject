using UnityEngine;

public class TeleportNPC : InteractableObject
{
    public override bool CheckInteractable(GameObject requester)
    {
        // 플레이어 1이 업고 있는 상태가 아니라면 false 리턴
        return false;
    }

    public InteractFailTalkBubble failHandler;
    public SO_FailHandleInfo failHandleInfo;

    public SceneChanger myChanger;

    public override void ExecuteOnSuccess(GameObject requester)
    {
        {
            myChanger.StartScene();
        }
    }


    public override void ExecuteOnFail(GameObject requester)
    {
        if(failHandler != null)
        {
            failHandler.FailHandle(this.gameObject, failHandleInfo);
        }
    }
}
