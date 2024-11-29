using UnityEngine;

public class LimbRideState : LimbAbillityState
{
    public LimbRideState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        Limb.spriteRenderer.enabled = false;
        Limb.RB.gravityScale = 0;

        if (Limb.isOwned)
        {
            if (Limb.isServer)
            {
                container.isRiding = true;
                GameManager.instance.Pdcontainer.carryupcall = false;
            }
            else
            {
                Limb.CmdSetisRiding(true);
                Limb.CmdSetCarryUpCall(false);
            }
        }

        isAbillityDone = true;
    }
}
