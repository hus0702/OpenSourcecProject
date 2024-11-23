using UnityEngine;

public class LimbRidingShotState : LimbAbillityState
{
    public Vector3 mousePosition;
    public Vector3 bulletrotation;
    public LimbRidingShotState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata, animBoolName)
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
        GameManager.instance.LimbData.attackInput = false;
        Limb.InputHandler.StartCoroutine(Limb.InputHandler.stopshotinput(GameManager.instance.LimbData.ShotDelay));
        mousePosition = limbdata.mousePosition;
        bulletrotation = mousePosition - Limb.transform.position;
        var bullet = Limb.Instantiate(Limb.BulletPrefab, Limb.transform.position + bulletrotation.normalized, Quaternion.Euler(bulletrotation));
        bullet.GetComponent<Rigidbody2D>().linearVelocity = (bulletrotation).normalized * limbdata.bulletspeed;

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
