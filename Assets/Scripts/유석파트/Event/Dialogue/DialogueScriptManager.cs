using UnityEngine;

public class DialogueScriptManager : MonoBehaviour
{
    public static DialogueScriptManager Instance { get; private set; }
    public int index;
    public DialogueScript scriptList;

    void Awake()
    {
        if(Instance == null) Instance = this;
        index = 0;
    }

    public string PopScrpit()
    {
        return scriptList.PopScrpit(index++);
    }
}
