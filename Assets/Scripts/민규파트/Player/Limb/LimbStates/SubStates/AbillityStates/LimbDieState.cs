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
        if (isAnimationFinished)
        {
            //Respawn ÇÔ¼ö

            container.Hp = 10;
            isAbillityDone = true;
        }
        else
            Debug.Log(isAnimationFinished);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
