using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Dialogue Script", menuName = "ScriptableObject/DialogueScript", order = 0)]
public class DialogueScript : ScriptableObject
{
    //public List<string> dialogues;
}

[System.Serializable]
public class DialogueScriptItem
{
    public string talker; // 그냥 알아보기 쉬우라고 넣어 놓음.
    public string script; // 대사
}