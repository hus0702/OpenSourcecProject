using UnityEngine;

public class PlayerPutDownState : PlayerAbillityState
{
    public PlayerPutDownState(Player player, PlayerStateMachine stateMachine, PlayerDataContainer container, string animBoolName) : base(player, stateMachine, container, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (player.isOwned)
        {
            if (player.isServer)
            {
                container.iscarrying = false;
            }
            else
            {
                player.CmdSetIsCarrying(false);
            }
        }

        isAbillityDone = true;

    }
}
