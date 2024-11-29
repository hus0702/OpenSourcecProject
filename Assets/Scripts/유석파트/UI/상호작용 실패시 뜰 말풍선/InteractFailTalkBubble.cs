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

    public void FailHandle(GameObject requester)
    {
        Show(requester);
    }
    public void FailHandle(GameObject requester, string message)
    {
        Show(requester, message);
    }
    public void FailHandle(GameObject requester, Sprite spriteToShow)
    {
        Show(requester, spriteToShow);
    }

    public void Show(GameObject requester)
    {
        talkBubbleObject.SetActive(true);
        timer = timerInitVal;
    }
    public void Show(GameObject requester,string message)
    {
        Show(requester);
        imageBubble.SetActive(false);
        textBubble.SetActive(true);
        text.text = message;
    }

    public void Show(GameObject requester,Sprite spriteToShow)
    {
        Show(requester);
        imageBubble.SetActive(true);
        textBubble.SetActive(false);
        this.spriteToShow.sprite = spriteToShow;
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
