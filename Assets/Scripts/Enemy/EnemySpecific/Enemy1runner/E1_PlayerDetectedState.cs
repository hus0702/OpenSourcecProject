using Unity.VisualScripting;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    private Collider2D hit;
    private Player blind;
    private Limb limb;

    public E1_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
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
        
        hit = entity.CheckAttackPlayer();

        if(hit != null)
        {
            blind = hit.GetComponent<Player>();
            Debug.Log("Player hit!");
            entity.PlayAttackSound();
            /*player1 데미지 함수*/
            blind.TakingDamage(enemy.damageAmount);
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
            limb.TakingDamage(enemy.damageAmount);
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }

        if(!(isPlayerInMaxAgroRange || isPlayerInMaxAgroRange2))
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
