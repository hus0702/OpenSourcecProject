using UnityEngine;
using Mirror;

public class BlindRenderManager : MonoBehaviour
{
    void Awake()
    {
        bool isBlind = CheckIsBlind();

        if(isBlind)
        {
            // 일단 맵에 존재하는 Light를 다 꺼야 함.
            // 어? 근데... 응? 흠....
        }
    }

    private bool CheckIsBlind(){
        PlayerObjectController localPlayer = NetworkClient.localPlayer.GetComponent<PlayerObjectController>();
        return localPlayer.Role == PlayerObjectController.Blind;
    }
}
