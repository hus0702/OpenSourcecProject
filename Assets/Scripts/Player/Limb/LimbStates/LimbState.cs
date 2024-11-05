using UnityEngine;

public class LimbState
{
    protected Limb Limb;
    protected LimbStateMachine stateMachine;

    protected bool isAnimationFinished;

    protected float startTime;


    private string animBoolName;

    public LimbState(Limb Limb, LimbStateMachine stateMachine)
    {
        this.Limb = Limb;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        DoCheck();
        startTime = Time.time;
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {

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