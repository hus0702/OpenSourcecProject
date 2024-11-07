using UnityEngine;

public class LimbGroundedState : LimbState
{
    
    public LimbGroundedState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata, animBoolName)
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

        if (GameManager.instance.PlayerData.carryupcall && Limb.CheckIftouchBlind())
        {
            stateMachine.LimbChangeState(Limb.RideState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
