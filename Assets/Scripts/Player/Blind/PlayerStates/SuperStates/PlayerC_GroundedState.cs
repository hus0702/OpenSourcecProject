using UnityEngine;

public class PlayerC_GroundedState : PlayerState
{
    public PlayerC_GroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    protected int xinput;

    protected bool PutDownInput;
    protected bool ThrowInput;

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
        PutDownInput = playerData.putdowncall;
        ThrowInput = playerData.throwcall;
        base.LogicUpdate();
        xinput = player.InputHandler.NormInputX;

        if (PutDownInput)
        {
            stateMachine.playerChangeState(player.PutDownState);
        }

        if(ThrowInput) 
        {
            stateMachine.playerChangeState(player.ThrowState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
