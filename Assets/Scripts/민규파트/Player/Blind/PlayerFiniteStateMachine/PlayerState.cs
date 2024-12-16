using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerDataContainer container;

    protected bool isAnimationFinished;

    protected float startTime;


    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.container = container;
        this.animBoolName = animBoolName;

    }

    public virtual void Enter()
    {
        DoCheck();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;

    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
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

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    } 
}
