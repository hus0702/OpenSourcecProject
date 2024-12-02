using UnityEngine;

public class LimbholdinggunidleState : LimbGroundedState
{
    public LimbholdinggunidleState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
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
        if (xinput != 0f)
        {
            stateMachine.LimbChangeState(Limb.MoveState);
        }
        if (container.holdingitem != 1)
        {
            stateMachine.LimbChangeState(Limb.IdleState);
        }
        if (container.attackInput)
        {
            stateMachine.LimbChangeState(Limb.ShotState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
