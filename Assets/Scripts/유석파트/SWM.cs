using UnityEngine;
using Mirror;

public class SWM : NetworkBehaviour
{

/*
        이 함수만 호출하시면 됩니다!

        인자 설명 : 
            sfxNum : 재생하고 싶은 효과음의 enum 번호를 int 로 변환해서 넣어주세요
            isShouldCheckCollider : 그냥 false 로 하시면 됩니다
            sourceOfSound : 소리가 나야 하는 장소의 GameObject 를 담아주세요
            power : 음파의 반지름입니다
            duration : 음파의 지속 시간입니다
*/
    public void MakeSoundwave(int sfxNum, bool isShouldCheckCollider, GameObject sourceOfSound , float power , float duration)
    {
        if(sourceOfSound == null) Debug.LogError("최상단 인터페이스 : sourceOfSound가 null입니다");
        else Debug.Log("$$$$$$$$$$$$ SWM MakeSoundWave : 난 분명 줬다 $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        RpcMakeSoundwave(sfxNum, isShouldCheckCollider, sourceOfSound.transform.position, power, duration);
    }

    private static SWM instance;
    public static SWM Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("SoundWaveManager : Instance가 초기화되지 않았습니다.");
                return null;
            }
            else return instance;
        }
    }

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }






    [ClientRpc] private void RpcMakeSoundwave(int sfxNum, bool isShouldCheckCollider, Vector3 sourceOfSound , float power , float duration)
    {
        if(sourceOfSound == null) Debug.LogError("SWM : sourceOf  SOOOOOOOund가 null입니다!");
        SoundWaveManager.Instance.MakeSoundWave(sfxNum, isShouldCheckCollider, sourceOfSound, power, duration);
    }
}
