using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ElectricPlatform : DangerousObject
{
    [SyncVar(hook = nameof(HookIsActiveChanged))]public bool isActive;
    private void HookIsActiveChanged(bool oldVal, bool newVal)
    {
        if(newVal == true)
        {
            On();
        }
        else
        {
            Off();
        }
    }

    public Light2D electricEffect;

    public void Off()
    {
        electricEffect.gameObject.SetActive(false);
        InActiveInteract();
    }

    public void On()
    {
        electricEffect.gameObject.SetActive(true);
        ActiveInteract();
    }
}
