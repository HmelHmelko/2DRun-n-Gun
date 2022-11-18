using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

    public void SemiChangeState(PlayerState state, PlayerState newState)
    {
        currentState.LogicUpdate();
        state = currentState;
        newState.Enter();
    }
}
