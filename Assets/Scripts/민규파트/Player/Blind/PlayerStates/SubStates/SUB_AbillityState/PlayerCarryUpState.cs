using UnityEngine;

public class PlayerCarryUpState : PlayerAbillityState
{
    public PlayerCarryUpState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.isOwned)
        {
            if (player.isServer)
            {
                container.iscarrying = true;
                container.carryupcall = false;
            }
            else
            {
                player.CmdSetIsCarrying(true);
                player.CmdSetCarryUpCall(false);
            }
        }
        container.iscarrying = true;
        container.carryupcall = false;
        player.InputHandler.StartCoroutine(player.InputHandler.stopcarryinput(0.3f));
        isAbillityDone = true;
    }
}
