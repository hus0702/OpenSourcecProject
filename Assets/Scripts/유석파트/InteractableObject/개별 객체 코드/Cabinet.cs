using UnityEngine;
using Mirror;
using System.Collections;

public class Cabinet : InteractableObject
{
    private Animator myAnimator;

    Vector3 positionToFix;

    GameObject objInMe = null; 
    float gravityScale;

    protected override void Awake()
    {
        base.Awake();
        myAnimator = GetComponent<Animator>();
        positionToFix = new Vector3(gameObject.transform.position.x , gameObject.transform.position.y + 0.3f, gameObject.transform.position.z);
    }

    public override void ExecuteOnSuccess(GameObject requester)
    {
        base.ExecuteOnSuccess(requester);

        myAnimator.SetBool("isIdle", false);
        myAnimator.SetBool("isInteract", true); // 일단 애니메이션 재생

        if(!objInMe)
        {
            Debug.Log(requester.name + "을 캐비넷에 넣습니다.");




            if (requester.tag == "Blind")
            {
                Player player = requester.GetComponent<Player>();
                player.SetObjectHandlingMe(this);
            }
            else if (requester.tag == "Limb")
            {
                Limb player = requester.GetComponent<Limb>();
                player.SetObjectHandlingMe(this);
            }


            SetPlayerInvisible(requester);


            objInMe = requester;
        }
        else
        {
            Debug.Log(objInMe.name + "을 캐비넷에서 빼겠습니다.");


            objInMe = null; // 그냥 이렇게만 해주면 될 듯.

            SetPlayerVisible(requester);
            /*
                바로 다시 들어갈 수 있도록 하는건 금지하겠음. 쿨타임을 좀 줄 것.
            */
            InActiveInteract();
            StartCoroutine(Cooldown());
        }
    }

    [ClientRpc] void SetPlayerInvisible(GameObject requester)
    {
        requester.GetComponent<Collider2D>().enabled = false;

        Rigidbody2D objRigidBody = requester.GetComponent<Rigidbody2D>();
        gravityScale = objRigidBody.gravityScale;
        objRigidBody.gravityScale = 0;

        ChangeAlpha(requester, 0);
    }
    [ClientRpc] void SetPlayerVisible(GameObject requester)
    {
        requester.GetComponent<Collider2D>().enabled = true;

        Rigidbody2D objRigidBody = requester.GetComponent<Rigidbody2D>();
        objRigidBody.gravityScale = gravityScale;

        ChangeAlpha(requester, 1);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.1f);
        ActiveInteract();
    }

    private void ChangeAlpha(GameObject target, float alpha)
    {
        SpriteRenderer targetRenderer = target.GetComponent<SpriteRenderer>();

        var targetColor = targetRenderer.color;
        targetColor.a = alpha;
        targetRenderer.color = targetColor;
    }

    public override bool CheckInteractable(GameObject requester)
    {
        if(objInMe)
        {
            if(requester != objInMe)
            {
                return false;
            }
        }
        // 위의 경우들이 아니라면 언제나 가능.
        return true;
    }

    void Update()
    {
        if(objInMe)
        {
            objInMe.transform.position = positionToFix; 
        }
    }
    
    public void OnFinishAnimation()
    {
        myAnimator.SetBool("isIdle", true);
        myAnimator.SetBool("isInteract", false);
    }
}
