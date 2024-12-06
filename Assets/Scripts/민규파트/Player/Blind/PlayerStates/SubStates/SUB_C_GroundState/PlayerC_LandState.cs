using UnityEngine;

public class PlayerC_LandState : PlayerC_GroundedState
{
    public PlayerC_LandState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        GameManager.instance.CmdPlaySoundOnClient(AudioManager.Sfx.BlindLand);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xinput != 0)
        {
            stateMachine.playerChangeState(player.c_moveState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.playerChangeState(player.c_idleState);
        }
    }
}
