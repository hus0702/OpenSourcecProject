using UnityEngine;

public class LimbAbillityState : LimbState
{
    protected bool isAbillityDone;
    private bool isGrounded;

    public LimbAbillityState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
    {
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
        isAbillityDone = false;
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
            if (container.isRiding)
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
