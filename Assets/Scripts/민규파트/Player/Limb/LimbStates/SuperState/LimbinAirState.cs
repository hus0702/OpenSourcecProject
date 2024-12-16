using UnityEngine;
using UnityEngine.Windows;

public class LimbinAirState : LimbState
{
    private bool isGrounded;
    private int xInput;

    public LimbinAirState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
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
        xInput = container.NormInputX;
        Limb.CheckifShouldflip(xInput);
        Limb.SetVelocityX(container.movementVelocity * xInput);
        if (isGrounded)
        {
            Limb.LimpLand();
            stateMachine.LimbChangeState(Limb.IdleState);
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
