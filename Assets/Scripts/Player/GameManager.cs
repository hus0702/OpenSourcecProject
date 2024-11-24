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
}
