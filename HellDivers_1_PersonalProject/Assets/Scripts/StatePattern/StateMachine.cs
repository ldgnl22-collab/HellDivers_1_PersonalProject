using UnityEngine;

public class StateMachine
{
    [SerializeField] private IState _currentState;
    
    public IState CurrentState => _currentState;

    public void Initialize(IState state)
    {
        _currentState = state;
    }

    public void ChangeState(IState state)
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void Execute()
    {
        _currentState.Execute();
    }
}
