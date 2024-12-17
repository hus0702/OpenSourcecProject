using UnityEngine;

public class SoundWaveMakable : MonoBehaviour
{
    public GameObject soundSource;
    public bool active = true; // 직접 끄지 않는 이상은 true 임.
    public int sfxNum;
    public bool isShouldCheckCollider;

    private PlayerObjectController playerObjectController;

    public void MakeSound()
    {
        if(!active) return;

        //TODO 아아아주 먼 미래에는 개선해야 함.
        playerObjectController = GetComponent<PlayerObjectController>();

        if(playerObjectController != null)
        {
            if(playerObjectController.Role == PlayerObjectController.Limp) return;
        }

        SoundWaveManager.Instance.MakeSoundWave(sfxNum, isShouldCheckCollider, soundSource, 4f, 0.8f);
    }
}
