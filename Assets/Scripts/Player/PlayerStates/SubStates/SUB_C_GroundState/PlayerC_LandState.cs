using UnityEngine;

public class PlayerC_LandState : PlayerC_GroundedState
{
    public PlayerC_LandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xinput != 0)
        {
            stateMachine.ChangeState(player.c_moveState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.c_idleState);
        }
    }
}
