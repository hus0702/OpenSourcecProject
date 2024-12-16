using UnityEngine;
using Mirror;

public class ItemGiver : InteractableObject
{
    public string itemToGive;


    public InteractFailTalkBubble successHandler;
    public SO_FailHandleInfo successHandleInfo;

    public override void ExecuteOnSuccess(GameObject requester)
    {
        // 플레이어한테 아이템을 전달해주면 됩니다.
        Debug.Log("아이템을 플레이어에게 전달합니다.");

        if(itemToGive == "총")
        {
            if(requester.tag == "Limb")
                GameManager.instance.Ldcontainer.itemset[1] = true;
            else if(requester.tag == "Blind")
            {
                if(GameManager.instance.Pdcontainer.iscarrying)
                {   
                    GameManager.instance.Ldcontainer.itemset[1] = true;
                }
                else return;
            }
        }
        else if(itemToGive == "드릴")
        {
            if(requester.tag == "Limb")
                GameManager.instance.Ldcontainer.itemset[3] = true;
            else if(requester.tag == "Blind")
            {
                if(GameManager.instance.Pdcontainer.iscarrying)
                {   
                    GameManager.instance.Ldcontainer.itemset[3] = true;
                }
                else return;
            }
        }
        else if(itemToGive == "카드키")
        {
            if(requester.tag == "Limb")
                GameManager.instance.Ldcontainer.itemset[2] = true;
            else if(requester.tag == "Blind")
            {
                if(GameManager.instance.Pdcontainer.iscarrying)
                {   
                    if(GameManager.instance.Pdcontainer.itemset[1])
                    {
                        GameManager.instance.Ldcontainer.itemset[2] = true;
                    }
                    else
                    {
                        GameManager.instance.Pdcontainer.itemset[1] = true;
                    }
                }
                else
                {
                    if(!GameManager.instance.Pdcontainer.itemset[1])
                        GameManager.instance.Pdcontainer.itemset[1] = true;
                    else return;
                }
            }
        }
        else return;

        if(successHandler != null)
        {
            successHandler.FailHandle(this.gameObject, successHandleInfo);
        }

        InActiveInteract();
    }
}
