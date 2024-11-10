using UnityEngine;

public class LimbAbillityState : LimbState
{
    protected bool isAbillityDone;

    private bool isGrounded;
    public LimbAbillityState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata, animBoolName)
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

        if ((isAbillityDone))
        {
            if (limbdata.isRiding)
            {
                stateMachine.LimbChangeState(Limb.RidingState);
            }
            else
            {
                if (!isGrounded)
                {
                    stateMachine.LimbChangeState(Limb.inAirState);
                }
                else
                {
                    stateMachine.LimbChangeState(Limb.IdleState);
                }
                
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}