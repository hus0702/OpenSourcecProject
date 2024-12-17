using UnityEngine;
using Mirror;

public class ItemGiver : InteractableObject
{
    public string itemToGive;

    [SyncVar(hook = nameof(HookIsGivable))] bool isGivable = true;
    private void HookIsGivable(bool ov, bool nv)
    {
        InActiveInteract();
    }

    public InteractFailTalkBubble successHandler;
    public SO_FailHandleInfo successHandleInfo;

    public override void ExecuteOnSuccess(GameObject requester)
    {
        // 플레이어한테 아이템을 전달해주면 됩니다.
        Debug.Log("아이템을 플레이어에게 전달합니다.");

        if(itemToGive == "총")
        {
            if(requester.tag == "Limb")
                GameManager.instance.Ldcontainer.item1 = true;
            else if(requester.tag == "Blind")
            {
                if(GameManager.instance.Pdcontainer.iscarrying)
                {   
                    GameManager.instance.Ldcontainer.item1 = true;
                }
                else return;
            }
        }
        else if(itemToGive == "드릴")
        {
            if(requester.tag == "Limb")
                GameManager.instance.Ldcontainer.item3 = true;
            else if(requester.tag == "Blind")
            {
                if(GameManager.instance.Pdcontainer.iscarrying)
                {   
                    GameManager.instance.Ldcontainer.item3 = true;
                }
                else return;
            }
        }
        else if(itemToGive == "카드키")
        {
            if(requester.tag == "Limb")
            {
                if(GameManager.instance.Ldcontainer.item2) return;
                GameManager.instance.Ldcontainer.item2 = true;
            }
            else if(requester.tag == "Blind")
            {
                if(GameManager.instance.Pdcontainer.iscarrying)
                {   
                    if(GameManager.instance.Pdcontainer.item1)
                    {
                        return;
                        GameManager.instance.Ldcontainer.item2 = true;
                    }
                    else
                    {
                        GameManager.instance.Pdcontainer.item1 = true;
                    }
                }
                else
                {
                    if(!GameManager.instance.Pdcontainer.item1)
                        GameManager.instance.Pdcontainer.item1 = true;
                    else return;
                }
            }
        }
        else return;

        if(successHandler != null)
        {
            successHandler.FailHandle(this.gameObject, successHandleInfo);
        }

        ServerSetIsGivable();
    }

    [Command(requiresAuthority = false)] public void ServerSetIsGivable()
    {
        isGivable = false;
    }
}
