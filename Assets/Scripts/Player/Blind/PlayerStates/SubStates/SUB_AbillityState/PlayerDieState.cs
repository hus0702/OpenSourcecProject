using UnityEngine;

public class PlayerDieState : PlayerAbillityState
{
    public PlayerDieState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
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
            //Respawn ÇÔ¼ö

            //ÀÓ½Ã ÄÚµå
            Debug.Log("Á×¾ú´Ù ²Ð");
            container.Hp = 10;
            isAbillityDone = true;
        }
        else
            Debug.Log(isAnimationFinished);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
