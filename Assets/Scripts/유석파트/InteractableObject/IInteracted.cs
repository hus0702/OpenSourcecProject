using UnityEngine;
using Mirror;
using Steamworks;

public interface IInteracted
{
    /* 상호작용당할 오브젝트들은 모두 아래의 함수를 호출하게 됩니다. */
    // 이 때 인자로 IInteractable 이든 뭐든 '상호작용가능한' 인터페이스를 implements 하는 객체를 받습니다.
    // 이 녀석의 정보를 읽거나 이 녀석의 특정 함수를 호출하여 상호작용이 이루어질 수 있도록 합시다!
    //TODO 협업 필요

    // public void Interact(IInteractable requester);
    public void Interact(GameObject requester); // 지금은 임시 함수로 작업하도록 합니다.
}
