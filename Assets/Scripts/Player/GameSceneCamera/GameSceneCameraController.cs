using Mirror;
using UnityEngine;

public class GameSceneCameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Transform target;
    void Start()
    {
        if (NetworkClient.localPlayer != null)
        { 
            target = NetworkClient.localPlayer.transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + new Vector3(0, 0, -10); // 예시 오프셋
        }
    }
}
