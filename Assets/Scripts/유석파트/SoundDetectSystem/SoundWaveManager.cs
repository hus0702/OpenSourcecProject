using Unity.VisualScripting;
using UnityEngine;
using Mirror;

public class SoundWaveManager : MonoBehaviour
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
    public void MakeSoundWave(int sfxNum, bool isShouldCheckCollider, Vector3 sourceOfSound , float power , float duration)
    {
        Debug.Log("Manager : 음파 생성");

/*
        if(NetworkClient.localPlayer != null)
        {
            RpcMakeSoundWave(sfxNum, isShouldCheckCollider, sourceOfSound, power, duration);
            return;
        }
*/
        SoundMakeNode node = soundMakeNodePool.GetObject().GetComponent<SoundMakeNode>();
        
        if(node == null)
        {
            Debug.LogError("SoundWaveManager : 노드를 풀에서 가져올 수 없습니다.");
            return;
        }

        node.setMyPool(soundMakeNodePool);
        // 하나 가져왔다면

        if(sourceOfSound == null) Debug.LogError("SoundWaveManager : sourceOfSound 가 null입니다!");
        node.MakeSound(sfxNum, isShouldCheckCollider, sourceOfSound, power, duration);
    }

/*
    [ClientRpc] private void RpcMakeSoundWave(int sfxNum, bool isShouldCheckCollider, GameObject sourceOfSound , float power , float duration)
    {
        SoundMakeNode node = soundMakeNodePool.GetObject().GetComponent<SoundMakeNode>();
        
        if(node == null)
        {
            Debug.LogError("SoundWaveManager : 노드를 풀에서 가져올 수 없습니다.");
            return;
        }

        node.setMyPool(soundMakeNodePool);
        // 하나 가져왔다면
        node.MakeSound(sfxNum, isShouldCheckCollider, sourceOfSound, power, duration);
    }
*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public ObjectPool soundMakeNodePool;

    public bool isBlind;

    private static SoundWaveManager instance;
    public static SoundWaveManager Instance
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


    bool isChecking = true;
    void Update()
    {
        if(isChecking)
        {
            if(NetworkClient.localPlayer)
            {
                if(NetworkClient.localPlayer.GetComponent<PlayerObjectController>().Role == PlayerObjectController.Blind)
                {
                    isBlind = true;
                }
                else
                {
                    isBlind = false;
                }
                isChecking = false;
            }
        }
    }
}
