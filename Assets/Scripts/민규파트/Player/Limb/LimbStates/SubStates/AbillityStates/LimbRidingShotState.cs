using UnityEngine;

public class LimbRidingShotState : LimbAbillityState
{
    public Vector3 mousePosition;
    public Vector3 bulletrotation;

    public LimbRidingShotState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
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
        Limb.LimpShot();
        if (Limb.isOwned)
        {
            if (Limb.isServer)
            {
                container.attackInput = false;
            }
            else
            {
                Limb.CmdSetattackInput(false);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            isAbillityDone = true;
        }


    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
