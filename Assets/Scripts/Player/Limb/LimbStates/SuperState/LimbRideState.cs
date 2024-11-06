using UnityEngine;

public class LimbRideState : LimbState
{
    public LimbRideState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata,animBoolName)
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
        Debug.Log("Ridestate¿‘º∫");
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
