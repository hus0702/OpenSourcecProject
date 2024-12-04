using UnityEngine;

public class MakeSoundTrigger : Trigger
{
    public SoundMakeNode node;
    public override void ActiveTrigger()
    {
        node.MakeSound(this.gameObject, 4f, 1.5f);
    }

    private void OnEnable() {
        ActiveTrigger();    
    }
}
