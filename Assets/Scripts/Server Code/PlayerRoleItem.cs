using UnityEngine;
using UnityEngine.UI;

public class PlayerRoleItem : MonoBehaviour
{
    public string playerName;
    public int ConnectionID;
    public Text playerNameText;

    void Start()
    {
        gameObject.SetActive(false);
        Debug.Log("아이템 초기화됨. 활성화 상태 : " + gameObject.activeSelf);
    }

    public void ChangeRoleActive()
    {
        Debug.Log("옳은 역활. 활성화합니다.");
        gameObject.SetActive(true);
    }
    public void ChangeRoleNotActive()
    {
        Debug.Log("역할과 맞지 않는 활성화를 비활성화합니다.");
        gameObject.SetActive(false);
    }
    public void SetPlayerValues()
    {
        playerNameText.text = playerName;
    }
}
