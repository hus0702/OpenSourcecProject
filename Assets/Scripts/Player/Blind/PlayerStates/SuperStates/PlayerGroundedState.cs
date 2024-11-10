using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xinput;

    protected bool sinput;

    private bool JumpInput;

    private bool CarryUpInput;

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

        xinput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        CarryUpInput = GameManager.instance.PlayerData.carryupcall;
        sinput = player.InputHandler.SitInput;

        if (JumpInput)
        {
            stateMachine.playerChangeState(player.JumpState);
        }
        if (CarryUpInput && player.CheckIftouchLimb())
        {
            Debug.Log("carryUp ȣ��");
            stateMachine.playerChangeState(player.carryUpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
