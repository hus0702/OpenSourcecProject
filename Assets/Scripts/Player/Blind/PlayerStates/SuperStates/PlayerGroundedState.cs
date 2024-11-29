using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xinput;

    protected bool sinput;

    private bool JumpInput;

    private bool CarryUpInput;

    private bool ladderInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
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

        xinput = container.NormInputX;
        JumpInput = container.JumpInput;
        CarryUpInput = container.carryupcall;
        ladderInput = container.ladderUp;
        sinput = container.SitInput;

        if (JumpInput)
        {
            stateMachine.playerChangeState(player.JumpState);
        }
        if (!player.CheckIfGrounded())
        {
            stateMachine.playerChangeState(player.InAirState);
        }
        if (CarryUpInput && player.CheckIftouchLimb())
        {
            stateMachine.playerChangeState(player.carryUpState);
        }
        if (ladderInput && player.CheckIftouchLadder())
        {
            stateMachine.playerChangeState(player.climbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
