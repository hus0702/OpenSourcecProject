using UnityEngine;

public class MakeSoundTrigger : Trigger
{
    public int sfxNum;
    public bool isShouldCheckCollider;
    public override void ActiveTrigger()
    {
        SoundWaveManager.Instance.MakeSoundWave(sfxNum, isShouldCheckCollider, this.gameObject, 4f, 1.5f);
    }
}
