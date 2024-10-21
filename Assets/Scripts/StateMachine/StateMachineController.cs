using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController
{
    public State currentState;

    public void Initialize(State state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
