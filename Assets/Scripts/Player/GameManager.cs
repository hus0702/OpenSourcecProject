using Mirror;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public PlayerDataContainer Pdcontainer;
    public LimbDataContainer Ldcontainer;

    private void Awake()
    {
        // 일반적인 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("나는 서버야");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("클라이언트인데유");
    }

    [Server]
    public void AssignAuthority(NetworkConnectionToClient conn)
    {
        // NetworkIdentity 컴포넌트가 필요함
        NetworkIdentity networkIdentity = GetComponent<NetworkIdentity>();

        if (networkIdentity != null && conn != null)
        {
            networkIdentity.AssignClientAuthority(conn);
            Debug.Log("권한이 클라이언트에게 부여되었습니다.");
        }
        else
        {
            Debug.LogError("NetworkIdentity를 찾을 수 없거나 유효하지 않은 클라이언트 연결입니다.");
        }
    }

    //[Server]
    //public void RemoveAuthority(NetworkConnectionToClient conn)
    //{
    //    // NetworkIdentity 컴포넌트가 필요함
    //    NetworkIdentity networkIdentity = GetComponent<NetworkIdentity>();

    //    if (networkIdentity != null && conn != null)
    //    {
    //        networkIdentity.RemoveClientAuthority(conn);
    //        Debug.Log("클라이언트 권한이 제거되었습니다.");
    //    }
    //    else
    //    {
    //        Debug.LogError("NetworkIdentity를 찾을 수 없거나 유효하지 않은 클라이언트 연결입니다.");
    //    }
    //}
}
