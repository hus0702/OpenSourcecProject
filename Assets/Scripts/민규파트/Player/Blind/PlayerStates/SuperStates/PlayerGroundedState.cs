using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xinput;

    protected bool sinput;

    private bool JumpInput;

    private bool CarryUpInput;

    private bool ladderInput;

    private bool InteractInput;

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
        InteractInput = container.InteractInput;

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
        if (ladderInput && player.CheckIftouchLadder() != null)
        {
            player.Blindclimb();
            stateMachine.playerChangeState(player.climbState);
        }
        if (container.Hp <= 0)
        {
            stateMachine.playerChangeState(player.DieState);
        }
        if (InteractInput)
        {
            player.Interact();
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
