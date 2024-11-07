using Mirror;
using UnityEditor.VersionControl;
using UnityEngine;

public class LimbRidingState : LimbState
{
    public bool throwInput;
    public bool PutDownInput;
    public LimbRidingState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata,animBoolName)
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

        if (GameManager.instance.PlayerData.putdowncall)
        {
            stateMachine.LimbChangeState(Limb.ThrowState);
        }

        if(GameManager.instance.PlayerData.throwcall)
        {
            stateMachine.LimbChangeState(Limb.PutDownState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
