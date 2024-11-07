using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LimbThrowState : LimbAbillityState
{
    public float acceleration = 10f;
    private bool isGrounded;
    public LimbThrowState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata, animBoolName)
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
        Vector2 force = Limb.transform.forward * acceleration;
        Limb.RB.AddForce(force);
        limbdata.isRiding = false;
        isAbillityDone = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
