using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
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
        player.SetVelocityX(container.movementVelocity * xinput);

        if (sinput)
        {
            stateMachine.playerChangeState(player.SitMoveState);
        }
        else
        {
            if (xinput == 0f)
            {
                stateMachine.playerChangeState(player.IdleState);
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
