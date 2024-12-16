using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ElectricPlatform : DangerousObject
{
    [SyncVar(hook = nameof(HookIsActiveChanged))]private bool isActive;
    private void HookIsActiveChanged(bool oldVal, bool newVal)
    {
        Debug.Log("전기장판에 대한 isActive 변경 감지! : " + newVal);
        if(newVal == true)
        {
            On();
        }
        else
        {
            Off();
        }
    }
    [Command(requiresAuthority = false)] public void SetIsActive(bool val)
    {
        isActive = val;
    }

    public Light2D electricEffect;

    public void On()
    {
        Debug.Log("전기장판 켜짐");
        electricEffect.gameObject.SetActive(true);
        ActiveInteract();
    }

    public void Off()
    {
        Debug.Log("전기장판 꺼짐");
        electricEffect.gameObject.SetActive(false);
        InActiveInteract();
    }
}
