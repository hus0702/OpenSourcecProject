using UnityEngine;

public class PlayerC_MoveState : PlayerC_GroundedState
{
    public PlayerC_MoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        player.CheckifShouldflip(xinput);
        player.SetVelocityX(playerData.C_movementVelocity * xinput);

        if (xinput == 0f)
        {
            stateMachine.playerChangeState(player.c_idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
