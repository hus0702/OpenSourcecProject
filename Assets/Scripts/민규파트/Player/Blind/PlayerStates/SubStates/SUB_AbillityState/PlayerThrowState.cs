using UnityEngine;

public class PlayerThrowState : PlayerAbillityState
{
    public PlayerThrowState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
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
        player.InputHandler.StartCoroutine(player.InputHandler.stopcarryinput(0.3f));
        if (player.isOwned)
        {
            if (player.isServer)
            {
                container.throwcall = false;
            }
            else
            {
                player.CmdSetThrowCall(false);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            isAbillityDone = true;
            if (player.isServer)
            {
                container.iscarrying = false;
            }
            else
            {
                player.CmdSetIsCarrying(false);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
