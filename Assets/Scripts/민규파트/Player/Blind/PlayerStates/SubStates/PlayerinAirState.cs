using UnityEngine;

public class PlayerinAirState : PlayerState
{

    private bool isGrounded;
    private int xInput;
    private bool ladderInput;

    public PlayerinAirState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CheckIfGrounded();
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

        xInput = container.NormInputX;
        ladderInput = container.ladderUp;
        if (ladderInput && player.CheckIftouchLadder())
        {
            stateMachine.playerChangeState(player.climbState);
        }

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.playerChangeState(player.LandState);
        }
        else
        {
            player.CheckifShouldflip(xInput);
            player.SetVelocityX(container.movementVelocity * xInput);
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
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
