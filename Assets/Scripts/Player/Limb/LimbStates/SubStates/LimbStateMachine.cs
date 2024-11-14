using UnityEngine;

public class LimbStateMachine
{

    public LimbState CurrentState { get; private set; }

    public void Initialize(LimbState startingstate)
    {
        CurrentState = startingstate;
        CurrentState.Enter();
    }

    public void ChangeState(LimbState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

}
