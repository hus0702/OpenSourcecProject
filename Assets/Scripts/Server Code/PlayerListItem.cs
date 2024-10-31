using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using Mirror;

public class PlayerListItem : MonoBehaviour
{
    public string PlayerName;
    public int ConnectionID;
    public ulong PlayerSteamID;
    private bool AvatarReceived;

    public Text PlayerNameText;
    public RawImage PlayerIcon;
    public Image PlayerReadyImage;
    public bool Ready;

    protected Callback<AvatarImageLoaded_t> ImageLoaded;

    void Start()
    {
        ImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
    }

    public void ChangeReadyStatus()
    {
        if(Ready)
        {
            Color color = PlayerReadyImage.GetComponent<Image>().color;
            color.a = 1f;
            PlayerReadyImage.color = color;
        }
        else
        {
            Color color = PlayerReadyImage.GetComponent<Image>().color;
            color.a = 0f;
            PlayerReadyImage.color = color;
        }
    }

    public void SetPlayerValues()
    {
        PlayerNameText.text = PlayerName;
        if(!AvatarReceived) GetPlayerIcon();
        ChangeReadyStatus();
    }

    void GetPlayerIcon()
    {
        int ImageID = SteamFriends.GetLargeFriendAvatar((CSteamID)PlayerSteamID); // 이 함수의 호출로 인해 이미지를 로드하게 됨. 그럼 콜백이 발동.
        if(ImageID == -1) return;
        PlayerIcon.texture = GetSteamImageAsTexture(ImageID);
    }

    private void OnImageLoaded(AvatarImageLoaded_t callback)
    {
        if(callback.m_steamID.m_SteamID == PlayerSteamID) // 우리껄 로드했을 때
            PlayerIcon.texture = GetSteamImageAsTexture(callback.m_iImage); // 플레이어 아이콘의 텍스쳐를 불러온걸로 변경!
        else return;
    }

    /*
        스팀 유틸을 이용해서 스팀 유저의 프로필 이미지를 로드하는 함수.
    */
    private Texture2D GetSteamImageAsTexture(int iImage)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
        if(isValid)
        {
            byte[] image = new byte[width * height * 4];
            isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

            if(isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }
        AvatarReceived = true;
        return texture;
    }
    
}
