using UnityEngine;

public class PlayerAbillityState : PlayerState
{

    protected bool isAbillityDone;

    private bool isGrounded;

    public PlayerAbillityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        isAbillityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if ((isAbillityDone))
        {
            if (playerData.iscarrying)
            {
                if (isGrounded && player.CurrentVelocity.y < 0.01f)
                {
                    stateMachine.ChangeState(player.c_LandState);
                }
                else
                {
                    stateMachine.ChangeState(player.c_InAirState);
                }
            }
            else
            {
                if (isGrounded && player.CurrentVelocity.y < 0.01f)
                {
                    stateMachine.ChangeState(player.LandState);
                }
                else
                {
                    stateMachine.ChangeState(player.InAirState);
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
