using UnityEngine;

public class LimbRideState : LimbAbillityState
{
    public LimbRideState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        limbdata.isRiding = true;
        GameManager.instance.PlayerData.carryupcall = false;
        isAbillityDone = true;
    }
}
