using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractFailTalkBubble : MonoBehaviour, IFailHandlable
{
    public GameObject talkBubbleObject;
    public Image spriteToShow;
    public TextMeshProUGUI text;

    public GameObject imageBubble;
    public GameObject textBubble;

    float timerInitVal = 1.5f;
    float timer = 0;


    public PlayerHeadTracker headTracker;

    public void FailHandle(GameObject requester, SO_FailHandleInfo failHandleInfo)
    {

        Debug.Log("실패 처리 말풍선 호출!");

        headTracker.FollowMe(requester);
        headTracker.yOffset = failHandleInfo.yoffset;

        switch (failHandleInfo.failHandleType)
        {
            case 1:
                Show(requester);
                break;
            case 2:
                if (failHandleInfo.message == null) Debug.LogError(gameObject.name + "에 에러 메시지가 없어요!");
                Show(requester, failHandleInfo.message);
                break;
            case 3:
                if (failHandleInfo.image == null) Debug.LogError(gameObject.name + "에 에러용 이미지가 없어요!");
                Show(requester, failHandleInfo.image);
                break;
            default:
                break;
        }
    }

    public void Show(GameObject requester)
    {
        Debug.Log("실패 처리 말풍선을 띄웠습니다!!");
        talkBubbleObject.SetActive(true);
        timer = timerInitVal;
    }
    public void Show(GameObject requester,string message)
    {
        Show(requester);
        imageBubble.SetActive(false);
        textBubble.SetActive(true);
        text.text = message;
        LayoutRebuilder.ForceRebuildLayoutImmediate(talkBubbleObject.GetComponent<RectTransform>());
    }

    public void Show(GameObject requester,Sprite spriteToShow)
    {
        Show(requester);
        imageBubble.SetActive(true);
        textBubble.SetActive(false);
        this.spriteToShow.sprite = spriteToShow;
        LayoutRebuilder.ForceRebuildLayoutImmediate(talkBubbleObject.GetComponent<RectTransform>());
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 0;
                talkBubbleObject.SetActive(false);
            }
        }
    }
}
