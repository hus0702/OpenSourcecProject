using UnityEngine;

public class DisplayFixer : MonoBehaviour
{
    private float currentAspect;
    private float goodAspect = 16f/9f;

    void Awake()
    {
        DontDestroyOnLoad(this);

#if UNITY_STANDALONE
        Screen.SetResolution(1600, 900, false);
        Screen.fullScreen = false;
#endif
    }


    void Update()
    {
#if UNITY_STANDALONE

        currentAspect = (float)Screen.width/Screen.height;

        if(Mathf.Abs(currentAspect - goodAspect) > 0.01f)
        {
            // 무언가 하나는 잘 되어 있을거임.
            if(currentAspect > goodAspect)
            {
                // 가로 길이가 더 길어! 그러므로 세로에 맞춰서 가로를 조정해야 함.
                int adjustedWidth = Mathf.RoundToInt(Screen.height * goodAspect);
                Screen.SetResolution(adjustedWidth, Screen.height, false);
            }
            else
            {
                // 높이가 더 크네? 그러면 너비에 맞게 높이를 줄여!
                Screen.SetResolution(Screen.width, Mathf.RoundToInt(Screen.width / goodAspect), false);
            }
        }

#endif
    }
}
