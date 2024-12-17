using UnityEngine;

public class MakeSoundTrigger : Trigger
{
    public int sfxNum;
    public bool isShouldCheckCollider;
    public float power;


    public bool isLocal;


    public override void ActiveTrigger()
    {
        if(!isLocal) SWM.Instance.MakeSoundwave(sfxNum, isShouldCheckCollider, this.gameObject, power, 0.8f);
        else SoundWaveManager.Instance.MakeSoundWave(sfxNum, isShouldCheckCollider, this.gameObject.transform.position, power, 0.8f);
    }
}
