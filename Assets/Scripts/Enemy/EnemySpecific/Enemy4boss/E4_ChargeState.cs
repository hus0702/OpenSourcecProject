using UnityEngine;

public class E4_ChargeState : State
{
    private Enemy4 enemy;
    private bool isDetectingLedge;
    private Collider2D hit;
    private Player blind;
    private Limb limb;
    
    private bool isWall;

    public float chargeSpeed = 3f;
    public float chargeAttackSpeed = 7f;


    public E4_ChargeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Enemy4 enemy) : base(etity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        enemy.SetVelocity(0f);
        isWall = enemy.CheckWall();
        base.Enter();
        entity.WaitCoroutine(chargeSpeed);
        enemy.anim.SetBool(animBoolName, false);
        enemy.anim.SetBool("chargeAttack",true);
        enemy.SetVelocity(chargeAttackSpeed);
    }

    public override void Exit()
    {
        enemy.anim.SetBool("chargeAttack",false);
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isWall)
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