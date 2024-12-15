using UnityEngine;

public class TeleportNPC : InteractableObject
{
    public override bool CheckInteractable(GameObject requester)
    {
        if(GameManager.instance.Pdcontainer.iscarrying) 
        {
            return true;
        // 플레이어 1이 업고 있는 상태가 아니라면 false 리턴
        }
        else
        {
            Debug.Log("함께 말을 거세요!");
            return false;
        } 
    }

    public InteractFailTalkBubble failHandler;
    public SO_FailHandleInfo failHandleInfo;

    public SceneChanger myChanger;

    public override void ExecuteOnSuccess(GameObject requester)
    {
        {
            //myChanger.StartScene();
        
            //34.5 , 11.5

            requester.transform.position = new Vector3(34.5f, 11.5f, 0f);
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
