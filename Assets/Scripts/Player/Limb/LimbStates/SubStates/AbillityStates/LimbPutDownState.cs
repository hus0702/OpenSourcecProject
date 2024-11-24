using UnityEngine;

public class LimbPutDownState : LimbAbillityState
{
    public LimbPutDownState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
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
        if (Limb.isServer)
        {
            container.isRiding = false;
            GameManager.instance.Pdcontainer.putdowncall = false;
        }
        else
        {
            container.CmdSetisRiding(false);
            GameManager.instance.Pdcontainer.CmdSetPutDownCall(false);
        }
        
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
