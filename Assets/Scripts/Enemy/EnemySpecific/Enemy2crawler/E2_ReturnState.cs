using UnityEngine;

public class E2_ReturnState : State
{
    private Enemy2 enemy;
    private bool isPlayerInMinAgroRange;

    public E2_ReturnState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Enemy2 enemy) : base(etity, stateMachine, animBoolName)
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
        enemy.ResetToInitialXPosition();
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();

        if(enemy.initialX == enemy.aliveGO.transform.position.x)
        {
            /*climb*/
            enemy.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
