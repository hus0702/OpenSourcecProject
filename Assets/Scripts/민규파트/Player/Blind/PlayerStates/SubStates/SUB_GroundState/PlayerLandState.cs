using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.BlindLand();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xinput != 0)
        {
            stateMachine.playerChangeState(player.MoveState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.playerChangeState(player.IdleState);
        }
    }
}
