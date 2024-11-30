using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : NetworkBehaviour
{
    public enum State
    {
        Walking,
        Chasing,
        Attack
    }

    public State currentState;
    
    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed;

    [SerializeField]
    private Transform 
        groundCheck,
        wallCheck;
    [SerializeField]
    private LayerMask whatIsGround;

    private int facingDirection;

    private Vector2 movement;

    private bool
        groundDetected,
        wallDetected;

        private GameObject alive;
        private Rigidbody2D aliveRb;

        private void Start()
        {
            alive = transform.Find("Alive").gameObject;
            aliveRb = alive.GetComponent<Rigidbody2D>();

            facingDirection = 1;
        }
    private void Update()
    {
        switch (currentState)
        {
            case State.Walking :
                UpdateWalkingState();
                break;
            case State.Chasing :
                UpdateChasingState();
                break;
            case State.Attack :
                UpdateAttackState();
                break;
            
        }
    }

    //--WALKING STATE---------------------------------------------

    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if(!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.linearVelocity.y);
            aliveRb.linearVelocity = movement;
        }
    }

    private void ExitWalkingState()
    {
        
    }

    //--CHASING STATE---------------------------------------------

    private void EnterChasingState()
    {

    }

    private void UpdateChasingState()
    {
        
    }

    private void ExitChasingState()
    {
        
    }

    //--ATTACK STATE---------------------------------------------

    private void EnterAttackState()
    {

    }

    private void UpdateAttackState()
    {
        
    }

    private void ExitAttackState()
    {
        
    }

    //--OTHER FUNCTIONS--------------------------

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void SwitchState(State state)
    {
        switch(currentState)
        {
            case State.Walking :
                ExitWalkingState();
                break;
            case State.Chasing :
                ExitChasingState();
                break;
            case State.Attack :
                ExitAttackState();
                break;
        }

        switch (state)
        {
            case State.Walking :
                EnterWalkingState();
                break;
            case State.Chasing :
                EnterChasingState();
                break;
            case State.Attack :
                EnterAttackState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}