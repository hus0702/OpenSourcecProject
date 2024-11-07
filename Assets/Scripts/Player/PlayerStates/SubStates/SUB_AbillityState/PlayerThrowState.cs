using UnityEngine;

public class PlayerThrowState : PlayerAbillityState
{
    public PlayerThrowState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerData.iscarrying = false;
        playerData.carryupcall = false;
        playerData.throwcall = false;
        isAbillityDone = true;
    }

}
