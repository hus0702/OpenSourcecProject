using UnityEngine;

public class PlayerClimbState : PlayerAbillityState
{
    public PlayerClimbState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        if (player.isOwned)
        {
            if (player.isServer)
            {
                container.isclimbing = true;
            }
            else
            {
                player.CmdSetIsClimbing(true);
            }
            player.Blindclimb();
        }
        player.RB.gravityScale = 0;
        player.SetVelocityX(0);
        player.SetVelocityY(0);
        isAbillityDone = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
