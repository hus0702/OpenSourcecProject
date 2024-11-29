using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LimbThrowState : LimbAbillityState
{
    private bool isGrounded;
    private float throwtime;

    public LimbThrowState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
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
        throwtime = GameManager.instance.Pdcontainer.throwinputtime;
        if (throwtime > 1)
            throwtime = 1;
        Limb.SetVelocityX(12 * throwtime * container.FacingDirection);
        Limb.SetVelocityY(4 + 2*throwtime);

        if (Limb.isOwned)
        {
            if (Limb.isServer)
            {
                container.isRiding = false;
                GameManager.instance.Pdcontainer.throwcall = false;
            }
            else
            {
                Limb.CmdSetisRiding(false);
                Limb.CmdSetThrowCall(false);
            }
        }
        Limb.spriteRenderer.enabled = true;
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
