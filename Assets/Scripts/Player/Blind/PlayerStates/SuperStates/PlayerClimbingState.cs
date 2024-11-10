using UnityEngine;

public class PlayerClimbingState : PlayerState
{
    public bool Up;
    public bool Down;
    public bool Jumpinput;
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
        Up = player.InputHandler.ladderUp;
        Down = player.InputHandler.ladderDown;
        Jumpinput = player.InputHandler.JumpInput;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

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

        if (Jumpinput)
        {
            playerData.isclimbing = false;
            stateMachine.playerChangeState(player.JumpState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
