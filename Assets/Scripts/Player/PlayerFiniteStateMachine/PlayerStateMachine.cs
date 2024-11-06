using UnityEngine;
using Mirror;
using UnityEngine.InputSystem.LowLevel;

public class PlayerStateMachine
{

    public PlayerState playerCurrentState { get; private set; }
    public LimbState LimbCurrentState { get; private set; }

    public Limb limb;

    public bool Carryinput;

    public void PlayerInitialize(PlayerState startingstate)
    { 
        playerCurrentState = startingstate;
        playerCurrentState.Enter();
    }

    public void LimbInitialize(LimbState startingstate)
    {
        LimbCurrentState = startingstate;
        LimbCurrentState.Enter();
    }

    public void playerChangeState(PlayerState newState)
    {
        Debug.Log(newState);

        if (newState is PlayerCarryUpState)
        {
            Carryinput = true;
        }

        playerCurrentState.Exit();
        playerCurrentState = newState;
        playerCurrentState.Enter();
    }

    public void LimbChangeState(LimbState newState)
    {
        Debug.Log("LimbState º¯°æ :" + newState);
        LimbCurrentState.Exit();
        LimbCurrentState = newState;
        LimbCurrentState.Enter();
    }
    public void LimbStateChangetoRide(LimbState newState)
    {
        if (Carryinput)
        {
            LimbChangeState(newState);
            Carryinput = false;
        }
    }
}
