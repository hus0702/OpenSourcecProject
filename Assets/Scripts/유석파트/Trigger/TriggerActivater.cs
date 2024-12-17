using System.Collections.Generic;
using UnityEngine;

public class TriggerActivater : MonoBehaviour
{
    public List<Trigger> triggers;

    public void ActiveTrigger()
    {
        foreach(Trigger trigger in triggers)
        {
            trigger.ActiveTrigger();
        }
    }
}
