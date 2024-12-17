using UnityEngine;

public class E2_DropState : State
{
    private Enemy2 enemy;
    private bool isDetectingLedge;
    private Collider2D hit;
    private Player blind;
    private Limb limb;

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

        hit = entity.CheckAttackPlayer();

        if(hit != null)
        {
            blind = hit.GetComponent<Player>();
            Debug.Log("Player hit!");
            entity.PlayAttackSound();
            /*player1 데미지 함수*/
            blind.TakingDamage(10);
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }

        hit = entity.CheckAttackPlayer2();
        
        if(hit != null)
        {
            limb = hit.GetComponent<Limb>();
            Debug.Log("Player2 hit!");
            entity.PlayAttackSound();
            /*player2 데미지 함수*/
            limb.TakingDamage(10);
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
