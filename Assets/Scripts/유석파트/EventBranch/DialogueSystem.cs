using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using UnityEditor;

public class DialogueSystem : MonoBehaviour
{
    /* �� �� ���� ���߿��� ��ũ��Ʈ�� ������ �� �ְ� �ϸ� ���� ��. */
    private float talkSpeed = 0.045f;

    [Header("�ؽ�Ʈ�ڽ�")]
    [SerializeField]
    private Text textBox;
    [Header("Ȱ��ȭ/��Ȱ��ȭ�� ��ü ������Ʈ")]
    [SerializeField]
    private GameObject TalkBubbleGameObject;

    private string script;

    /* ��ȭ ��ŵ, ��ȭ �ѱ�� ����� ���� ���� */
    bool isSkip = false;
    bool isEndDialogue = false;
    Coroutine currentEffect;

    /* ��ȭ ���� ������ �����ϱ� ���� �ʿ��� ���� */
    bool isHandling = false;
    string command; // ����ڰ� �±׷� �Է��� ��.

    public void Begin()
    {
        TalkBubbleGameObject.SetActive(true);

        /* ��ü���� �������� ����� ��ȭ ������ �ϳ� �޾ƿ;� �մϴ�! */

        textBox.text = ""; // ���� �ؽ�Ʈ�� ��������ϴ�.
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
