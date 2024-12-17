using System.Data;
using UnityEngine;

public class E3_MoveState : MoveState
{
    private Enemy3 enemy;
    public E3_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy3 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isPlayerInMinAgroRange|| isPlayerInMinAgroRange2)
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }

        else if(isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
