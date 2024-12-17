using Unity.VisualScripting;
using UnityEngine;

public class SoundWaveManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public ObjectPool soundMakeNodePool;

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

    public void MakeSoundWave(GameObject sourceOfSound , float power , float duration)
    {
        Debug.Log("Manager : 음파 생성");
        SoundMakeNode node = soundMakeNodePool.GetObject().GetComponent<SoundMakeNode>();
        
        if(node == null)
        {
            Debug.LogError("SoundWaveManager : 노드를 풀에서 가져올 수 없습니다.");
            return;
        }

        node.setMyPool(soundMakeNodePool);
        // 하나 가져왔다면
        node.MakeSound(sourceOfSound, power, duration);
    }
}
