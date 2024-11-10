using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
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
