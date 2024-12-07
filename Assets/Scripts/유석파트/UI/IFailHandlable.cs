using UnityEngine;

public interface IFailHandlable
{
    public void FailHandle(GameObject requester, SO_FailHandleInfo failHandleInfo);
}
