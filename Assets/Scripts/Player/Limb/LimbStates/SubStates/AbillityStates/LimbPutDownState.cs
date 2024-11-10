using UnityEngine;

public class LimbPutDownState : LimbAbillityState
{
    public LimbPutDownState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata, animBoolName)
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
        Limb.SetVelocityY(4);
        Limb.limbtransform.position = Limb.limbtransform.position;
        limbdata.isRiding = false;
        GameManager.instance.PlayerData.putdowncall = false;
        isAbillityDone = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        Limb.SetVelocityY(4);
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}