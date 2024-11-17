using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xinput;

    protected bool sinput;

    private bool JumpInput;

    private bool CarryUpInput;

    private bool ladderInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        xinput = GameManager.instance.PlayerData.NormInputX;
        JumpInput = GameManager.instance.PlayerData.JumpInput;
        CarryUpInput = GameManager.instance.PlayerData.carryupcall;
        ladderInput = GameManager.instance.PlayerData.ladderUp;
        sinput = GameManager.instance.PlayerData.SitInput;

        if (JumpInput)
        {
            stateMachine.playerChangeState(player.JumpState);
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
