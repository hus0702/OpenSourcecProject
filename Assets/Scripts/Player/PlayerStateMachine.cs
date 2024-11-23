using UnityEngine;
using Mirror;
using UnityEngine.InputSystem.LowLevel;

public class PlayerStateMachine
{

    public PlayerState playerCurrentState { get; private set; }
    public LimbState LimbCurrentState { get; private set; }


    public void PlayerInitialize(PlayerState startingstate, PlayerDataContainer data)
    { 
        playerCurrentState = startingstate;
        playerCurrentState.Enter();
    }

    public void LimbInitialize(LimbState startingstate, LimbDataContainer data)
    {
        LimbCurrentState = startingstate;
        LimbCurrentState.Enter();

    }

    public void playerChangeState(PlayerState newState)
    {

        playerCurrentState.Exit();
        playerCurrentState = newState;
        playerCurrentState.Enter();
    }

    public void LimbChangeState(LimbState newState)
    {
        LimbCurrentState.Exit();
        LimbCurrentState = newState;
        LimbCurrentState.Enter();
    }
}
