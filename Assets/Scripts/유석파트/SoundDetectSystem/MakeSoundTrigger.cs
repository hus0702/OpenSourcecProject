using UnityEngine;

public class MakeSoundTrigger : Trigger
{
    public override void ActiveTrigger()
    {
        SoundWaveManager.Instance.MakeSoundWave(this.gameObject, 4f, 1.5f);
    }
}
