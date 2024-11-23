using UnityEngine;
using UnityEngine.Windows;

public class LimbinAirState : LimbState
{
    private bool isGrounded;
    private int xInput;
    public LimbinAirState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata, animBoolName)
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
        isGrounded = Limb.CheckIfGrounded();
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
        xInput = limbdata.NormInputX;
        Limb.CheckifShouldflip(xInput);
        Limb.SetVelocityX(limbdata.movementVelocity * xInput);
        if (isGrounded)
        {
            stateMachine.LimbChangeState(Limb.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
