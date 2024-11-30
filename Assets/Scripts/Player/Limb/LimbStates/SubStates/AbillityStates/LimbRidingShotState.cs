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

        if (bulletrotation.y > 3) // 최대 총 각도 설정
        {
            bulletrotation.y = 3;
        }

        if (bulletrotation.y < -3)
        {
            bulletrotation.y = -3;
        }

        var bullet = Limb.Instantiate(Limb.BulletPrefab, GameManager.instance.Pdcontainer.position + bulletrotation.normalized, Quaternion.Euler(bulletrotation));
        bullet.GetComponent<Rigidbody2D>().linearVelocity = (bulletrotation).normalized * container.bulletspeed;

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
