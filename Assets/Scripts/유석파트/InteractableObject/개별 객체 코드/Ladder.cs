using UnityEngine;
using Mirror;

public class Ladder : InteractableObject
{
    /*  
        API : 

        사다리에 대해 Interact 를 호출하시면 
            1. 사다리는 플레이어의 Interact 를 자기를 인자로 호출 (하게 해주셨으면 좋겠습니다) 한다.
        
        플레이어가 Ladder State 에서 다른 State 로 Transition 될 때
        ladder . OnLadderOut ( 플레이어 ) ; 
        이걸 한 번 호출한다.

        아셔야 될건 이게 답니다! 협업할 때 봅시다. 
    */
    [SerializeField]private Collider2D colliderToPass;

    protected override void Awake()
    {
        base.Awake();
        if(colliderToPass == null) Debug.LogError("WARNING! 사다리에 매달렸을 때 통과하게 할 플랫폼이 할당되지 않았습니다!");
    }

    public override bool CheckInteractable(GameObject requester)
    {

        return false; // 더 이상 이 오브젝트와는 상호작용할 수 없기 때문에 비활성화.


        //return isColliderOverlap(requester.GetComponent<Collider2D>(), colliderToPass);
    }

    public override void ExecuteOnSuccess(GameObject requester)
    {
        // 플레이어의 State 를 LadderClimb State 등으로 변경할 것!
            // 혹은 그냥 requester . Interact 를 호출하거나. (물론 이 Ladder 객체를 인자로 보내야 함)
        Debug.Log("플레이어의 State 를 Ladder Climb 으로 바꾼다!!!");
        Debug.Log("희망사항 : 플레이어에 대해 requester . OnInteract(this) 등을 호출한다.");
        
        // 내 쪽에서 할건 이게 다임.
        Physics2D.IgnoreCollision(colliderToPass, requester.GetComponent<Collider2D>(), true);
    }

    public void OnTriggerExit2D(Collider2D outer){
        //if(outer.CompareTag("Player"))
        Physics2D.IgnoreCollision(colliderToPass, outer, false);
    }








    public void OnEnterLadder(GameObject playerGameObject)
    {
        /*
        Collider2D playerCollider = playerGameObject.GetComponent<Collider2D>();
        if(playerCollider == null) Debug.LogError("플레이어의 collider 가 null입니다!");
        if(colliderToPass == null) Debug.LogError("충돌을 무시할 플랫폼이 null입니다!");

        Debug.Log("충돌을 비활성화할 오브젝트 : " + playerGameObject.name + " , " + colliderToPass.gameObject.name);
        Physics2D.IgnoreCollision(colliderToPass, playerCollider);
        Debug.Log("충돌 가능 여부 : " + Physics2D.GetIgnoreCollision(colliderToPass, playerCollider));

        */


        // 위에꺼 작동 안 되는듯...? 이러면 이판사판이다
        colliderToPass.enabled = false; // 개잘작동함....
    }
    public void OnExitLadder(GameObject playerGameObject)
    {

        /*
        Debug.Log("충돌을 활성화할 오브젝트 : " + playerGameObject.name + " , " + colliderToPass.gameObject.name);
        Physics2D.IgnoreCollision(colliderToPass, playerGameObject.GetComponent<Collider2D>(), false);
        */
        colliderToPass.enabled = true;
    }
}
