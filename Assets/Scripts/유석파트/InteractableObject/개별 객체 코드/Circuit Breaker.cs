using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CircuitBreaker : InteractableObject
{
    [SyncVar(hook = nameof(HookIsActiveChanged))] bool isActive = true;
    public void HookIsActiveChanged(bool oldVal, bool newVal)
    {
        Debug.Log("차단기가 " + newVal + " 상태로 변경됐습니다.");

        if(newVal == true) platformToManage.On();
        else platformToManage.Off();

        foreach(GameObject item in gameObjectsToManage)
        {
            item.SetActive(newVal);
        }
    }

    public ElectricPlatform platformToManage;
    public List<GameObject> gameObjectsToManage = new List<GameObject>();

    public override void ExecuteOnSuccess(GameObject requester)
    {
        base.ExecuteOnSuccess(requester);
        isActive = !isActive;
    }
}
