using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LimbState
{
    protected Limb Limb;
    protected PlayerStateMachine stateMachine;
    protected LimbDataContainer container;
    protected PlayerDataContainer playercontainer;
    protected bool isAnimationFinished;
    GameObject targetObject;

    protected float startTime;
    private string animBoolName;
    private bool attackInput;

    public LimbState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName)
    {
        this.Limb = Limb;
        this.stateMachine = stateMachine;
        this.container = Limb.GetComponent<LimbDataContainer>();
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
        if (targetObject == null)
        {
            targetObject = GameObject.FindWithTag("Blind");
            playercontainer = targetObject.GetComponent<PlayerDataContainer>();
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

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }
    
}