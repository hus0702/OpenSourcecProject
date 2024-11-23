using UnityEngine;

public class PlayerSitState : PlayerGroundedState
{
    public PlayerSitState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
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
        player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!sinput)
        {
            stateMachine.playerChangeState(player.IdleState);
        }
        else
        {
            if (xinput != 0f)
            {
                stateMachine.playerChangeState(player.SitMoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
