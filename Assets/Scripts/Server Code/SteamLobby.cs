using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;

public class SteamLobby : MonoBehaviour
{
    public static SteamLobby Instance;

    //콜백함수들
    /*
        지식 ) Callback 은 Steamwork가 어떤 일을 수행할 때, 이를 감지하고 내가 원하는 일을 시키기 위한 객체.

        보통 Callback<>.Create(함수) // 이런 식으로 콜백을 추가할 수 있고, 이를 저장하기 위해 따로 변수 필드를 만들어 줬음.
    */
    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    protected Callback<LobbyEnter_t> LobbyEnter;


    public ulong CurrentLobbyID; // 로비 ID
    private const string HostAddressKey = "HostAddress";
    private CustomNetworkManager manager;


    void Start()
    {
        if(!SteamManager.Initialized) return; // 이거 실행되기 전에 반드시 스팀 API를 활성화할 것!

        if(Instance == null) Instance = this;

        manager = CustomNetworkManager.singleton as CustomNetworkManager;

        // 만들어준 콜백 함수를 콜백에 연결하자. 드디어 스팀워크가 해당 동작이 발생하면 콜백 함수를 호출할 수 있게 된다!
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }


    /*
        여기서부터 등록할 실제 함수들이 나오는데, 이 함수들은 반드시 첫 번째 인자로 Callback 클래스의 _t 값을 받음.
        이게 뭐냐면, 해당 콜백이 일어났을 때 서버가 우리한테 줘야 하는 상세 정보를 모두 담은 값이라고 보면 됨.
        여기에 온갖 정보가 다 담긴다!
    
        등장 함수 : 
            SetLobbyData : (로비ID, 키) 를 통해 (값)을 하나 저장함. 즉, 불러올 때는 (로비ID, 키) 이걸 키값으로 쓰면 됨.
    */
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK) return; // 로비가 잘 생성되면 EResult Enum 의 값인 k_EResultOK 를 반환하게 되어 있음. 이를 검사.

        manager.StartHost(); // 네트워크 매니저를 이용해서 서버 열기

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", SteamFriends.GetPersonaName().ToString());
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        // 다른 플레이어가 로비에 접속하고자 했을 때 이 콜백이 발동되게 할 예정.
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }
    
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        //그냥 누군가 로비에 들어오기만 하면 바로 얘가 호출될 예정.
        CurrentLobbyID = callback.m_ulSteamIDLobby;
        manager.SetCurrentLobbyID(CurrentLobbyID);
        
        // 딱 여기까진 공통.

        /* 클라이언트한테서만 호출될 함수 */
        if(NetworkServer.active) return; // 이걸 조건으로 하면 서버가 활성화되어 있는 서버 측은 로비 이미지에 대한 정보만 얻고 return 으로 끝남.
        // 즉 이 밑에부터는 클라이언트한테만 작동함
        manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey); // 호스트 주소로 저장했던 어드민의 스팀 아이디를 받아옴.
        manager.StartClient(); // 받아온 주소를 통해 서버에 클라이언트로 접속한다!
    }

// 실제 뷰
    public GameObject HostButton;
    public GameObject JoinButton;
    public GameObject ExitButton;

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 2); // 버튼이 눌리면 로비를 생성. 방에는 두 명만 들어올 수 있음.
    }

    public void JoinLobby()
    {
        // 심플하게 스팀 친구창을 띄워주는걸로 대체.
        SteamFriends.ActivateGameOverlay("friends");
    }
    
    public void ExitLobby()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
