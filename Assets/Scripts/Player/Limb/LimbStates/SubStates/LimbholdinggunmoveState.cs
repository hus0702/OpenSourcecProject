using UnityEngine;

public class LimbholdinggunmoveState : LimbGroundedState
{
    public LimbholdinggunmoveState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
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
        Limb.CheckifShouldflip(xinput);
        Limb.SetVelocityX(container.movementVelocity * xinput);
        if (xinput == 0f)
        {
            stateMachine.LimbChangeState(Limb.holdinggunidleState);
        }
        if (container.holdingitem != 1)
        {
            stateMachine.LimbChangeState(Limb.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
