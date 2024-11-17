using Mirror;
using Mirror.Examples.Common;
using UnityEngine;

public class GameSceneCameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameObject target;
    private NetworkIdentity ID;


    void Update()
    {
        

        if (target != null)
        {
            
            transform.position = target.transform.position + new Vector3(0, 0, -10); // 예시 오프셋
        }
    }
}
