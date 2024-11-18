using System.Diagnostics;
using UnityEngine;

public class PlayerJumpState : PlayerAbillityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.jumpVelocity);
        
        isAbillityDone = true;
    }
}
