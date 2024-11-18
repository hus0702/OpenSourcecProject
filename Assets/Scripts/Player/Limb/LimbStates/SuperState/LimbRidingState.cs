using Mirror;
using UnityEngine;

public class LimbRidingState : LimbState
{
    public bool throwInput;
    public bool PutDownInput;
    public float FacingDirection;
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
        FacingDirection = GameManager.instance.PlayerData.facingdirection;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if (GameManager.instance.PlayerData.facingdirection != FacingDirection)
        {
            FacingDirection *= -1;
            Limb.Flip();
        }
        base.LogicUpdate();
        if (!GameManager.instance.PlayerData.iscarrying)
        {
            if (GameManager.instance.PlayerData.putdowncall)
            {
                stateMachine.LimbChangeState(Limb.PutDownState);
            }
            else if(GameManager.instance.PlayerData.throwcall)
            {
                stateMachine.LimbChangeState(Limb.ThrowState);
            }
        }




    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
