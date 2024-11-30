using UnityEngine;

public class SceneChangeTrigger : Trigger
{
    public SceneChanger myChanger;

    public override void ActiveTrigger()
    {
        myChanger.StartScene();
    }
}
