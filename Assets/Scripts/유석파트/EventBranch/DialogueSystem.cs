using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using UnityEditor;

public class DialogueSystem : MonoBehaviour
{
    /* 이 값 또한 나중에는 스크립트로 조절할 수 있게 하면 좋을 듯. */
    private float talkSpeed = 0.045f;

    [Header("텍스트박스")]
    [SerializeField]
    private Text textBox;
    [Header("활성화/비활성화할 전체 오브젝트")]
    [SerializeField]
    private GameObject TalkBubbleGameObject;

    private string script;

    /* 대화 스킵, 대화 넘기기 기능을 위한 값들 */
    bool isSkip = false;
    bool isEndDialogue = false;
    Coroutine currentEffect;

    /* 대화 연출 도구를 구현하기 위해 필요한 값들 */
    bool isHandling = false;
    string command; // 사용자가 태그로 입력한 값.

    public void Begin()
    {
        TalkBubbleGameObject.SetActive(true);

        /* 객체에서 다음으로 출력할 대화 내용을 하나 받아와야 합니다! */

        textBox.text = ""; // 기존 텍스트는 비워버립니다.
        StartCoroutine(Talk());
    }

    IEnumerator Talk()
    {
        foreach(var letter in script)
        {
            if (letter == '<' && !isHandling)
                isHandling = true;
            else if (isHandling)
            {
                if (letter == '>')
                {
                    isHandling = false;
                    yield return StartCoroutine(CommandHandler(command));
                }
                else command += letter;
            }
        }
        yield return null;
    }

    IEnumerator CommandHandler(string command)
    {
        yield return null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
