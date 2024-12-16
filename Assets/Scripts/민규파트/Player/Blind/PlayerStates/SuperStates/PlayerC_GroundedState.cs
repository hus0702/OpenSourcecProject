using UnityEngine;

public class PlayerC_GroundedState : PlayerState
{
   

    protected int xinput;

    protected bool PutDownInput;
    protected bool ThrowInput;
    private bool ladderInput;
    private bool InteractInput;

    public PlayerC_GroundedState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
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
        PutDownInput = container.putdowncall;
        ThrowInput = container.throwcall;
        base.LogicUpdate();
        xinput = container.NormInputX;
        ladderInput = container.ladderUp;
        InteractInput = container.InteractInput;
        if (ladderInput && player.CheckIftouchLadder() != null)
        {
            
            player.Blindclimb();
            stateMachine.playerChangeState(player.climbState);
        }
        if (PutDownInput)
        {
            stateMachine.playerChangeState(player.PutDownState);
        }
        if(ThrowInput) 
        {
            stateMachine.playerChangeState(player.ThrowState);
        }
        if (container.Hp <= 0)
        {
            stateMachine.playerChangeState(player.DieState);
        }
        if (InteractInput)
        {
            player.Interact();
        }
        if (container.Hp <= 0)
        {
            stateMachine.playerChangeState(player.DieState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
