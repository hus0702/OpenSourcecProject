using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerDieState : PlayerAbillityState
{
    public PlayerDieState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
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
        

    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityX(0);
        if (isAnimationFinished)
        {
            if (player.isServer)
            {
                container.Hp = 10;
                container.Respawncall = true;
            }
            else
            {
                player.CmdChangeHp(10);
                player.CmdSetRespawncall(true);
            }
            //임시 코드
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
