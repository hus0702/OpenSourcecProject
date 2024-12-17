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
                GameManager.instance.Pdcontainer.Hp = 100000;
                container.Respawncall = true;
            }
            else
            {
                Limb.CmdChangeHp(10);
                Limb.CmdSetRespawncall(true);
            }
            
            isAbillityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
