using UnityEngine;

public class PlayerC_IdleState : PlayerC_GroundedState
{
    public PlayerC_IdleState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xinput != 0f)
        {
            stateMachine.playerChangeState(player.c_moveState);
        }
        if (GameManager.instance.Ldcontainer.holdingitem == 1)
        {
            stateMachine.playerChangeState(player.c_holdinggunmoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
