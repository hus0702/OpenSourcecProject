using UnityEngine;
using Mirror;
using UnityEngine.InputSystem.LowLevel;

public class PlayerStateMachine
{

    public PlayerState playerCurrentState { get; private set; }
    public LimbState LimbCurrentState { get; private set; }


    public void PlayerInitialize(PlayerState startingstate, PlayerData data)
    { 
        playerCurrentState = startingstate;
        playerCurrentState.Enter();
    }

    public void LimbInitialize(LimbState startingstate, LimbData data)
    {
        LimbCurrentState = startingstate;
        LimbCurrentState.Enter();

    }

    public void playerChangeState(PlayerState newState)
    {
        Debug.Log(newState);

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
    public void LimbStateChangetoRide(LimbState newState)
    {
        if (GameManager.instance.PlayerData.iscarrying)
        {
            Debug.Log("������� �Ǹ� ����!");
            LimbChangeState(newState);
        }
            
    }
}
