using UnityEngine;
using Mirror;
using Mirror.Examples.Common.Controllers.Tank;

public class Vent : Door
{

    public bool Debug_isPlayerHasDrill;

    [SyncVar(hook = nameof(HookIsOpened))] public bool isSyncOpen = false;
    private void HookIsOpened(bool oldVaule, bool newValue)
    {
        if(newValue == true)
        {
            Animator doorAnimator = GetComponent<Animator>();
            doorAnimator.SetBool("isIdle", false);
            doorAnimator.SetBool("isOpened", true);
        }
        isOpened = true;
    }
    public GameObject errorMessagePosition;

    public override bool CheckInteractable(GameObject requester)
    {
        if(!isOpened)
        {
            if(requester.tag == "Limb" && GameManager.instance.Ldcontainer.itemset[3])
            {
                return true;
            }
            else return false;
        }
        else return true;
    }

    public override void ExecuteOnSuccess(GameObject requester)
    {
        if(!isOpened)
        {
            // 여기에서는 벤트를 열어야 함.
            CmdSetIsSyncOpen(true);
        }
        else
        {
            if(requester.tag == "Limb") TransportRequester(requester);
        }
    }

    [Command(requiresAuthority =false)] public void CmdSetIsSyncOpen(bool val)
    {
        isSyncOpen = val;
    }

    public override void ExecuteOnFail(GameObject requester)
    {
        Debug.Log("문이 잠겨 있습니다.");
        // 실제로는 UI 로 말풍선 비슷하게 띄울 것. 필요한 아이템 이미지가 말풍선 안에 들어가고, 진동하게 만들 것.
        if(failHandler != null)
        {
            failHandler.FailHandle(errorMessagePosition, failHandleInfo);
        }
    }
}
