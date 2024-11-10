using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        player.SetVelocityX(playerData.movementVelocity * xinput);

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
