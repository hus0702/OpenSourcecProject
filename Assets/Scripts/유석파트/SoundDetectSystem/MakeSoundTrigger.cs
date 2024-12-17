using UnityEngine;

public class MakeSoundTrigger : Trigger
{
    public int sfxNum;
    public bool isShouldCheckCollider;
    public override void ActiveTrigger()
    {
        SoundWaveManager.Instance.MakeSoundWave(sfxNum, isShouldCheckCollider, this.gameObject.transform.position, 4f, 1.5f);
    }
}
