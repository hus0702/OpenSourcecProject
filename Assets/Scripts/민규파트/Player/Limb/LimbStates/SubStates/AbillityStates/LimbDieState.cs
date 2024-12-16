using UnityEngine;

public class LimbDieState : LimbAbillityState
{
    public LimbDieState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
    {
    }



    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Limb.SetVelocityX(0);
        if (isAnimationFinished)
        {
            if (Limb.isServer)
            {
                container.Hp = 10;
            }
            else
            {
                Limb.CmdChangeHp(10);
            }
            Limb.Respawn();
            GameManager.instance.Blind.GetComponent<Player>().Respawn();
            isAbillityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
