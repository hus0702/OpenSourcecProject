using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class LimbShotState : LimbAbillityState
{
    public Vector3 mousePosition;
    public Vector3 bulletrotation;

    public LimbShotState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
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
        Debug.Log("ShotState ¿‘º∫");

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
        Limb.InputHandler.StartCoroutine(Limb.InputHandler.stopshotinput(container.ShotDelay));
        mousePosition = container.mousePosition;
        bulletrotation = mousePosition - Limb.transform.position;
        var bullet = Limb.Instantiate(Limb.BulletPrefab, Limb.transform.position + new Vector3(container.FacingDirection,0,0), Quaternion.Euler(bulletrotation));
        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(container.FacingDirection, 0, 0) * container.bulletspeed;

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
            Debug.Log("ShotState ≥°");
            isAbillityDone = true;
        }

        
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
