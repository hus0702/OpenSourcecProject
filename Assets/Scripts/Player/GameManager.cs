using Mirror;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public PlayerDataContainer Pdcontainer;
    public LimbDataContainer Ldcontainer;

    private void Awake()
    {
        // �Ϲ����� �̱��� ���� ����
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
        Debug.Log("���� ������");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("Ŭ���̾�Ʈ�ε���");
    }

    [Server]
    public void AssignAuthority(NetworkConnectionToClient conn)
    {
        // NetworkIdentity ������Ʈ�� �ʿ���
        NetworkIdentity networkIdentity = GetComponent<NetworkIdentity>();

        if (networkIdentity != null && conn != null)
        {
            networkIdentity.AssignClientAuthority(conn);
            Debug.Log("������ Ŭ���̾�Ʈ���� �ο��Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogError("NetworkIdentity�� ã�� �� ���ų� ��ȿ���� ���� Ŭ���̾�Ʈ �����Դϴ�.");
        }
    }

    //[Server]
    //public void RemoveAuthority(NetworkConnectionToClient conn)
    //{
    //    // NetworkIdentity ������Ʈ�� �ʿ���
    //    NetworkIdentity networkIdentity = GetComponent<NetworkIdentity>();

    //    if (networkIdentity != null && conn != null)
    //    {
    //        networkIdentity.RemoveClientAuthority(conn);
    //        Debug.Log("Ŭ���̾�Ʈ ������ ���ŵǾ����ϴ�.");
    //    }
    //    else
    //    {
    //        Debug.LogError("NetworkIdentity�� ã�� �� ���ų� ��ȿ���� ���� Ŭ���̾�Ʈ �����Դϴ�.");
    //    }
    //}
}
