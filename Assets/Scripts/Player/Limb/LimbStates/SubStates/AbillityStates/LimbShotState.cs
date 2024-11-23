using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class LimbShotState : LimbAbillityState
{
    public Vector3 mousePosition;
    public Vector3 bulletrotation;
    float timecheck;
    public LimbShotState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata, animBoolName)
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
        Debug.Log("ShotState 입성");
        GameManager.instance.LimbData.attackInput = false;
        Limb.InputHandler.StartCoroutine(Limb.InputHandler.stopshotinput(GameManager.instance.LimbData.ShotDelay));
        mousePosition = limbdata.mousePosition;
        bulletrotation = mousePosition - Limb.transform.position;
        var bullet = Limb.Instantiate(Limb.BulletPrefab, Limb.transform.position + bulletrotation.normalized, Quaternion.Euler(bulletrotation));
        bullet.GetComponent<Rigidbody2D>().linearVelocity = (bulletrotation).normalized * limbdata.bulletspeed;
        timecheck = Time.time;
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
            Debug.Log("ShotState 끝");
            isAbillityDone = true;
        }
        else
            Debug.Log("애니메이션이 안끝나");

        if (Time.time - timecheck > 2)
        {
            AnimationFinishTrigger();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
