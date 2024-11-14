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
                if (playerData.isclimbing)
                {
                    stateMachine.playerChangeState(player.c_ClimbingState);
                }
                else
                {
                    if (isGrounded && player.CurrentVelocity.y < 0.1f)
                    {
                        stateMachine.playerChangeState(player.c_LandState);
                    }
                    else
                    {
                        stateMachine.playerChangeState(player.c_InAirState);
                    }
                }
            }
            else
            {
                if (playerData.isclimbing)
                {
                    stateMachine.playerChangeState(player.climbingState);
                }
                else
                {
                    if (isGrounded && player.CurrentVelocity.y < 0.1f)
                    {
                        stateMachine.playerChangeState(player.LandState);
                    }
                    else
                    {
                        stateMachine.playerChangeState(player.InAirState);
                    }
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
