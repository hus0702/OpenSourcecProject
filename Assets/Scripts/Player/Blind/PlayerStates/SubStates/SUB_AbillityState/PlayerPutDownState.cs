using UnityEngine;

public class PlayerPutDownState : PlayerAbillityState
{
    public PlayerPutDownState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerData.iscarrying = false;
        isAbillityDone = true;
    }
}
