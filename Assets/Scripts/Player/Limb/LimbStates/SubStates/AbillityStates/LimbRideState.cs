using UnityEngine;

public class LimbRideState : LimbAbillityState
{
    public LimbRideState(Limb Limb, PlayerStateMachine stateMachine, LimbDataContainer container, string animBoolName) : base(Limb, stateMachine, container, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
        
        isAbillityDone = true;
    }
}
