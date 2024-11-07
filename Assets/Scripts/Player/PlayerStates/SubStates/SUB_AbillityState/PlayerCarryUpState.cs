using UnityEngine;

public class PlayerCarryUpState : PlayerAbillityState
{
    public PlayerCarryUpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }
    public override void Enter()
    {
        base.Enter();
        playerData.iscarrying = true;
        isAbillityDone = true;
    }
}
