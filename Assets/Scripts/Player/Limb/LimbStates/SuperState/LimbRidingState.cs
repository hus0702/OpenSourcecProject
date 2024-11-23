using Mirror;
using UnityEngine;

public class LimbRidingState : LimbState
{
    public LimbRidingState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
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
        Limb.spriteRenderer.enabled = false;
        Limb.RB.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!playercontainer.iscarrying)
        {
            stateMachine.LimbChangeState(Limb.ThrowState);
        }
        if (container.attackInput)
        {
            stateMachine.LimbChangeState(Limb.RidingShotState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
