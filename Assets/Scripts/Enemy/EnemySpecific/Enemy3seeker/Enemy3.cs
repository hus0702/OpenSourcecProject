using UnityEngine;

public class Enemy3 : Entity
{
    public E3_IdleState idleState {get; private set;}
    public E3_MoveState moveState {get; private set;}
    public E3_PlayerDetectedState playerDetectedState {get; private set;}

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;


    public override void Start()
    {
        base.Start();

        moveState = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E3_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        
        stateMachine.Initialize(idleState);

    }
}
