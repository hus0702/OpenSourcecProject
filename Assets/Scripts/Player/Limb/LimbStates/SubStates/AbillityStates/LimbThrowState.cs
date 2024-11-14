using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LimbThrowState : LimbAbillityState
{
    private bool isGrounded;
    float throwtime;
    Vector2 forcevector;
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
        //forcevector = new Vector2(35,35);
        //Limb.RB.AddForce(forcevector, ForceMode2D.Impulse);
        Limb.SetVelocityX(12 * GameManager.instance.PlayerData.facingdirection);
        Limb.SetVelocityY(5);
        limbdata.isRiding = false;
        GameManager.instance.PlayerData.throwcall = false;
        

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();
        isAbillityDone = true;

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
