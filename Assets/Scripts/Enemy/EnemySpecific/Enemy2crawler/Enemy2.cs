using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState idleState {get; private set;}
    public E2_MoveState moveState {get; private set;}
    public E2_PlayerDetectedState playerDetectedState {get; private set;}

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;


    public override void Start()
    {
        base.Start();

        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        
        stateMachine.Initialize(idleState);

    }
}
