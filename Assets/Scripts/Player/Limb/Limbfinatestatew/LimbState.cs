using UnityEngine;

public class LimbState
{
    protected Limb Limb;
    protected PlayerStateMachine stateMachine;
    protected LimbData limbdata;

    protected bool isAnimationFinished;

    protected float startTime;
    private string animBoolName;
    private bool attackInput;

    public LimbState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName)
    {
        this.Limb = Limb;
        this.stateMachine = stateMachine;
        this.limbdata = limbdata;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoCheck();
        Limb.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        Limb.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        attackInput = Limb.InputHandler.attackInput;
        if (attackInput)
        {
            stateMachine.LimbChangeState(Limb.ShotState);
        }
    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    public virtual void DoCheck()
    {

    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}