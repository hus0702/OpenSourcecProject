using UnityEngine;

public class E2_DropState : State
{
    private Enemy2 enemy;
    private bool isDetectingLedge;

    public E2_DropState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Enemy2 enemy) : base(etity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        isDetectingLedge = enemy.CheckLedge();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isDetectingLedge = enemy.CheckLedge();
        
        if(isDetectingLedge)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if(enemy.CheckAttackPlayer())
        {
            Debug.Log("Player hit!");
            /*player1 데미지 함수(즉사)*/
            stateMachine.ChangeState(enemy.idleState);
        }

        if(enemy.CheckAttackPlayer2())
        {
            Debug.Log("Player hit!");
            /*player2 데미지 함수(즉사)*/
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
