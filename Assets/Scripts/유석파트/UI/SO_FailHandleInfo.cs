using UnityEngine;

[CreateAssetMenu(fileName = "Fail Handle Info", menuName = "ScriptableObjects/Fail Handle Info")]
public class SO_FailHandleInfo : ScriptableObject
{
    [Header("y 오프셋")]
    public float yoffset;

    [Header("1 : 그냥 띄우기\n 2 : 메시지를 띄우기\n 3 : 이미지를 띄우기")]
    public int failHandleType;

    [Header("실패시 띄울 메시지")]
    public string message;

    [Header("실패시 띄울 이미지")]
    public Sprite image;
}
