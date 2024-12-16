using System.Data;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState idleState {get; private set;}
    public E2_MoveState moveState {get; private set;}
    public E2_PlayerDetectedState playerDetectedState {get; private set;}
    public E2_ReturnState returnState {get; private set;}
    public E2_DropState dropState {get; private set;}

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;

    public float initialX;
    public float initialY;

    public override void Start()
    {
        base.Start();

        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        returnState = new E2_ReturnState(this, stateMachine, "return", this);
        dropState = new E2_DropState(this, stateMachine, "drop", this);

        initialX = aliveGO.transform.position.x;
        initialY = aliveGO.transform.position.y;

        stateMachine.Initialize(moveState);

    }

    public void ResetToInitialXPosition()
    {
        aliveGO.transform.position = Vector2.MoveTowards(aliveGO.transform.position, new Vector2(initialX, aliveGO.transform.position.y), moveStateData.movementSpeed * Time.deltaTime);
    }

    public void ResetToInitialYPosition()
    {
        aliveGO.transform.position = Vector2.MoveTowards(aliveGO.transform.position, new Vector2(aliveGO.transform.position.x, initialY), moveStateData.movementSpeed * Time.deltaTime);
    }
}
