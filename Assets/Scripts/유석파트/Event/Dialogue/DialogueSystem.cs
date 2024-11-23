using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    
    private float talkSpeed = 0.07f;

    [Header("스크립트가 들어갈 텍스트 박스")]
    [SerializeField]
    private TextMeshProUGUI textBox;
    [Header("대사가 미리 채워질 박스")]
    [SerializeField]
    private TextMeshProUGUI textBoxToPreprocess;
    [Header("활성화/비활성화할 말풍선의 GameObject")]
    [SerializeField]
    private GameObject TalkBubbleGameObject;

/*
    [Header("이 씬에서 플레이어들이 읽을 대본")]
    [SerializeField]
    private DialogueScript scriptList;
*/
    /* 건너뛰기, 연출 장치를 위한 맴버 */
    bool isSkip = false;
    bool isEndDialogue = false;
    Coroutine currentEffect;

    /* <> 태그 처리를 위한 명령어 */
    bool isHandling = false;
    string command; // 파싱할 태그가 저장될 문자열 맴버

    void Awake()
    {
        TalkBubbleGameObject.SetActive(false);
    }

    public void Begin()
    {
        Debug.Log("DialogueSystem:Begin");
        textBox.text = ""; // 일단 다 비운다.

        string script = DialogueScriptManager.Instance.PopScrpit();
        textBoxToPreprocess.text = TruncCommands(script); // 미리 대화박스를 채워준다.
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());

        TalkBubbleGameObject.SetActive(true);
        StartCoroutine(Talk(script));
    }

    IEnumerator Talk(string script)
    {
        foreach(var letter in script)
        {
            /* 태그를 처리해야 한다! 안 그러면 태그 문자 하나하나씩 출력이 돼버릴거임...
            
                이를 위해 해야 하는 것 : 태그는 한 글자씩 출력하지 말고 태그가 끝날 때까지 기다렸다가 모두 담아서 한꺼번에 뿌려야 함.
             */
            #region <> 태그 처리하는 부분
            if (letter == '<' && !isHandling)
            {
                isHandling = true;
                command = "<";
            }
            else if (isHandling)
            {
                command += letter;
                if (letter == '>')
                {
                    isHandling = false;
                    yield return StartCoroutine(CommandHandler(command));
                }
            }
            #endregion
            else
            {
                textBox.text += letter;

                if(isSkip) continue;
                yield return new WaitForSeconds(talkSpeed);
            }
        }
        isEndDialogue = true;
        //yield return new WaitForSeconds(1.5f);
        //Debug.Log(script + " 비활성화됨.");
        //TalkBubbleGameObject.SetActive(false);
    }

    private void OnEnable() {
        textBox.text = "   ";
        textBoxToPreprocess.text = "   ";
    }
    private void OnDisable() {
        textBox.text = "   ";
        textBoxToPreprocess.text = "   ";
    }

    private string TruncCommands(string script)
    {
        string scriptWithoutCommand = "";
        string truncCommand = "";
        bool truncIsHandling = false;
        foreach(var letter in script)
        {
            if (letter == '<' && !truncIsHandling)
            {
                truncIsHandling = true;
                truncCommand = "<";
            }
            else if (truncIsHandling)
            {
                truncCommand += letter;
                if (letter == '>')
                {
                    truncIsHandling = false;
                    if(truncCommand.Contains("sleep"))
                    {
                        // 걍 아무것도 하지 않고 없애버린다.
                    }
                    else
                    {
                        scriptWithoutCommand += truncCommand;
                    }
                }
            }
            else scriptWithoutCommand += letter;
        }
        return scriptWithoutCommand;
    }

    IEnumerator CommandHandler(string command)
    {
        List<string> arguments = new List<string>();

        #region arguments 에 괄호 안의 인자를 저장하는 중.....
        if(command.Contains("(")) // 어! 괄호? 
        {
            // 이건 내가 추가로 만든 연출용 기능이 포함된 태그. 즉 파싱을 좀 해야 한다.
            bool isArgumentFind = false;
            int index = 0;
            
            foreach(var c in command)
            {
                if(c == '(')
                {
                    isArgumentFind = true;
                    arguments.Add(""); // 첫 번째 아이템을 넣어서 글자를 추가할 수 있게 한다.
                }
                else if(isArgumentFind)
                {
                    if(c == ',') // 다른 인자를 감지했다!
                    {
                        arguments.Add(""); // 새로운 문자열을 하나 시작한다
                        index++;
                    }
                    else if(c == ' ') continue; // 공백을 감지하면 그냥 스킵
                    else if(c == ')') isArgumentFind = false; // 다 찾았으니까 끈다.
                    else
                    {
                        arguments[index] += c; // 인자의 글자로 하나 추가한다.
                    }
                }
            }

            if(isArgumentFind) Debug.LogError("DialogueSystem Error : 어떤 사용자 정의 태그에 ) 가 빠졌습니다!");
        }
        #endregion

        Debug.Log("Command : " + command);

        if(command.Contains("sleep"))
        {
            float sleepSec = float.Parse(arguments[0]);
            yield return StartCoroutine(WaitForSecondsSkipable(sleepSec));
        }
        else
        {
            // 얜 우리가 만든 태그가 아님. 원래 있던 거니까, 이걸 그냥 그대로 textBox 에 뿌려줘야 함!
            textBox.text += command;
        }

        yield return null;
    }

    IEnumerator WaitForSecondsSkipable(float seconds)
    {
        float curSec = seconds;

        while(curSec > 0 && !isSkip){
            yield return null;
            curSec -= Time.deltaTime;
        }
    }
}
