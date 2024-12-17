using Mirror;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class Entity : NetworkBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;
    public int facingDirection;
    public int damageAmount;
    public Rigidbody2D rb {get; private set;}
    public Animator anim { get; private set;}
    public GameObject aliveGO { get; private set;}

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform playerCheck2;

    private Vector2 velocityWorkspace;
    private SWM swm;

    public virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        damageAmount = 5;
        stateMachine = new FiniteStateMachine();
        swm = new SWM();
    }

    public virtual void Update()
    {
        if (isServer)
        {
            stateMachine.currentState.LogicUpdate();
        }
        
    }

    public virtual void FixedUpdate()
    {
        if (isServer)
        {
            stateMachine.currentState.PhysicsUpdate();
        }
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.linearVelocityY);
        rb.linearVelocity = velocityWorkspace;
    }

    public virtual void PlayAttackSound()
    {
    SWM.Instance.MakeSoundwave((int)AudioManager.Sfx.Enemygrawl, true, aliveGO, 6f, 0.8f);
    }

    IEnumerator WaitForAnimation(string animationName)
    {
        // 현재 상태 정보 가져오기
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // 애니메이션 재생 시작
        anim.Play(animationName);

        // 애니메이션이 끝날 때까지 대기
        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0); // 상태 정보 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        Debug.Log("애니메이션 종료!");
        // 여기서 다음 동작을 실행
    }

//************************************************************

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInDownAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, -aliveGO.transform.up, 10000f, entityData.whatIsPlayer);
    }

    public virtual Collider2D CheckAttackPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(playerCheck.position, entityData.attackRange, entityData.whatIsPlayer);
        return player;
    }

    public virtual bool CheckPlayerInMinAgroRange2()
    {
        return Physics2D.Raycast(playerCheck2.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer2);
    }

    public virtual bool CheckPlayerInMaxAgroRange2()
    {
        return Physics2D.Raycast(playerCheck2.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer2);
    }

    public virtual bool CheckPlayerInDownAgroRange2()
    {
        return Physics2D.Raycast(playerCheck2.position, -aliveGO.transform.up, 10000f, entityData.whatIsPlayer2);
    }

    public virtual Collider2D CheckAttackPlayer2()
    {
        Collider2D player = Physics2D.OverlapCircle(playerCheck2.position, entityData.attackRange, entityData.whatIsPlayer2);
        return player;
    }

//*********************************************************************************

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos() {
        
        Gizmos.DrawLine(wallCheck.position, wallCheck.position * (Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position * (Vector2.down * entityData.wallCheckDistance));
    }

    public IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSeconds(time); // 3초 대기
    }
}
