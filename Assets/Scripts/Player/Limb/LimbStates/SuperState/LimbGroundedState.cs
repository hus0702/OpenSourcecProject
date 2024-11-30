using Steamworks;
using UnityEngine;

public class LimbGroundedState : LimbState
{
    protected int xinput;

    public LimbGroundedState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
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
        xinput = container.NormInputX;
        if (GameManager.instance.Pdcontainer != null)
        {
            if (GameManager.instance.Pdcontainer.iscarrying && Limb.CheckIftouchBlind())
            {
                stateMachine.LimbChangeState(Limb.RideState);
            }
        }
        if (!Limb.CheckIfGrounded())
        {
            stateMachine.LimbChangeState(Limb.inAirState);
        }
        if (container.attackInput)
        {
            stateMachine.LimbChangeState(Limb.ShotState);
        }
        if (container.Hp <= 0)
        {
            stateMachine.LimbChangeState(Limb.DieState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
