using UnityEngine;

public class PlayerSitMoveState : PlayerGroundedState
{
    public PlayerSitMoveState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoCheck()
    {
        base.DoCheck();
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
        player.CheckifShouldflip(xinput);
        player.SetVelocityX(container.sitmovementVelocity * xinput);
        if (!sinput)
        {
            stateMachine.playerChangeState(player.MoveState);
        }
        else
        {
            if (xinput == 0f)
            {
                stateMachine.playerChangeState(player.SitState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
