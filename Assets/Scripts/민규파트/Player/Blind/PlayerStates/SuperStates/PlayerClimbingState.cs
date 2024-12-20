using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.Windows;

public class PlayerClimbingState : PlayerState
{
    private bool Up;
    private bool Down;
    private bool Jumpinput;
    private int xinput;

    public PlayerClimbingState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
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

        Up = container.ladderUp;
        Down = container.ladderDown;
        Jumpinput = container.JumpInput;
        xinput = container.NormInputX;

        player.SetVelocityX(container.movementVelocity * xinput);

        if (Up)
        {
            player.Anim.speed = 1;
            player.SetVelocityY(container.climbVelocity);
        }
        else if (Down)
        {
            player.Anim.speed = 1;
            player.SetVelocityY(container.climbVelocity * -1);
        }
        else
        {
            player.Anim.speed = 0;
            player.SetVelocityY(0);
        }

        if (!player.CheckIftouchLadder())
        {
            container.isclimbing = false;
            player.RB.gravityScale = 5;
            player.Anim.speed = 1;
            player.Blindclimboff();
            stateMachine.playerChangeState(player.InAirState);
        }
        if (container.Hp <= 0)
        {
            stateMachine.playerChangeState(player.DieState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
