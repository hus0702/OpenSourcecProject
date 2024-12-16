using System.Data;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private Enemy2 enemy;
    public E2_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy2 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        enemy.SetVelocity(0f);
        base.Enter();
        enemy.rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        /*climb*/
        if(enemy.initialY != enemy.aliveGO.transform.position.y)
        {
            enemy.ResetToInitialYPosition();
        }

        if(enemy.CheckPlayerInDownAgroRange())
        {
            enemy.rb.gravityScale = 1;
            stateMachine.ChangeState(enemy.dropState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
