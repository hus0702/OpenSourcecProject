using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LimbThrowState : LimbAbillityState
{
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
        Limb.transform.position = Limb.transform.position;
        limbdata.isRiding = false;
        GameManager.instance.PlayerData.throwcall = false;
        isAbillityDone = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        Limb.SetVelocityX(15 * GameManager.instance.PlayerData.facingdirection);
        Limb.SetVelocityY(10);
        base.LogicUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
