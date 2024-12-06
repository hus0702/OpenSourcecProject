using Unity.VisualScripting;
using UnityEngine;

public class SoundMakeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public ObjectPool soundMakeNodePool;

    private static SoundMakeManager instance;
    public static SoundMakeManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("SoundMakeManager").AddComponent<SoundMakeManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
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


    //public void Make
}
