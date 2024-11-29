using UnityEngine;

public interface IFailHandlable
{
    public void FailHandle(GameObject requester);
    public void FailHandle(GameObject requester, string message);
    public void FailHandle(GameObject requester, Sprite spriteToShow);
}
