using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.Windows;

public class PlayerClimbingState : PlayerState
{
    private bool Up;
    private bool Down;
    private bool Jumpinput;
    private int xinput;
    public PlayerClimbingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        Up = player.InputHandler.ladderUp;
        Down = player.InputHandler.ladderDown;
        Jumpinput = player.InputHandler.JumpInput;
        xinput = player.InputHandler.NormInputX;

        player.SetVelocityX(playerData.movementVelocity * xinput);

        if (Up)
        {
            player.SetVelocityY(playerData.climbVelocity);
        }
        else if (Down)
        {
            player.SetVelocityY(playerData.climbVelocity * -1);
        }
        else
        {
            player.SetVelocityY(0);
        }

        if (Jumpinput || !player.CheckIftouchLadder())
        {
            playerData.isclimbing = false;
            player.RB.gravityScale = 5;
            
            stateMachine.playerChangeState(player.InAirState);
        }
            
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
