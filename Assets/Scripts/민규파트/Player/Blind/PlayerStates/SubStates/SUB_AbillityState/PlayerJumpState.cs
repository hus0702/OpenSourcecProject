using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;

public class PlayerJumpState : PlayerAbillityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


        player.SetVelocityY(container.jumpVelocity);
        if (player.isOwned)
        {
            if (player.isServer)
            {
                container.JumpInput = false;
            }
            else
            {
                player.CmdSetJumpInput(false);
            }
        }
        isAbillityDone = true;
    }
}
