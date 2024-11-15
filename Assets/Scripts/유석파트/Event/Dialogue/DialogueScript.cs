using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dialogue Script", menuName = "ScriptableObject/DialogueScript", order = 0)]
public class DialogueScript : ScriptableObject
{
    public List<DialogueScriptItem> scripts;
    
    public string PopScrpit(int index)
    {
        string ret = scripts[index].script;
        return ret;
    }
}

[System.Serializable]
public class DialogueScriptItem
{
    [TextArea] public string script; // 대사
}