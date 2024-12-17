using UnityEngine;

public class Enemy4 : Entity
{
    public E4_IdleState idleState {get; private set;}
    public E4_MoveState moveState {get; private set;}
    public E4_PlayerDetectedState playerDetectedState {get; private set;}
    public E4_ChargeState chargeState {get; private set;}

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;

    public override void Start()
    {
        base.Start();

        moveState = new E4_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E4_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E4_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new E4_ChargeState(this,stateMachine, "charge", this);
        stateMachine.Initialize(idleState);

    }
}
