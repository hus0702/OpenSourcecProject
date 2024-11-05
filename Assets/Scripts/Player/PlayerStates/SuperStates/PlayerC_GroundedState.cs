using UnityEngine;

public class PlayerC_GroundedState : PlayerState
{
    public PlayerC_GroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    protected int xinput;

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

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
