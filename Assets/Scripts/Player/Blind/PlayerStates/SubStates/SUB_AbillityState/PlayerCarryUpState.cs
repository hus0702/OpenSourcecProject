using UnityEngine;

public class PlayerCarryUpState : PlayerAbillityState
{
    public PlayerCarryUpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }
    public override void Enter()
    {
        base.Enter();
        player.InputHandler.StartCoroutine(player.InputHandler.stopcarryinput(0.1f));
        playerData.iscarrying = true;
        isAbillityDone = true;
    }
}
