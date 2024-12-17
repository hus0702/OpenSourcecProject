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


    private Vector2 velocityWorkspace;
    
    [ServerCallback]
    public virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        damageAmount = 5;
        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.linearVelocityY);
        rb.linearVelocity = velocityWorkspace;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isServer)
        {
            if (collision.gameObject.CompareTag("Limb")) 
            {
                // Player의 TakingDamage 함수를 호출
                Limb player = collision.gameObject.GetComponent<Limb>();
                if (player != null)
                {
                    player.TakingDamage(damageAmount);
                }
            }
        }

         if (isServer) // 서버에서만 처리
        {
            if (collision.gameObject.CompareTag("Blind")) 
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.TakingDamage(damageAmount);
                }
            }
        }
    }

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

    public virtual bool CheckAttackPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(playerCheck.position, entityData.attackRange, entityData.whatIsPlayer);
        return player != null;
    }

    public virtual bool CheckPlayerInMinAgroRange2()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer2);
    }

    public virtual bool CheckPlayerInMaxAgroRange2()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer2);
    }

    public virtual bool CheckPlayerInDownAgroRange2()
    {
        return Physics2D.Raycast(playerCheck.position, -aliveGO.transform.up, 10000f, entityData.whatIsPlayer2);
    }

    public virtual bool CheckAttackPlayer2()
    {
        Collider2D player = Physics2D.OverlapCircle(playerCheck.position, entityData.attackRange, entityData.whatIsPlayer2);
        return player != null;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos() {
        
        Gizmos.DrawLine(wallCheck.position, wallCheck.position * (Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position * (Vector2.down * entityData.wallCheckDistance));
    }

    public IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2f); // 2초 대기
    }
}
