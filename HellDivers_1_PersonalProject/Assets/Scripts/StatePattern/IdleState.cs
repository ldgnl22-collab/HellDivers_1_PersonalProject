using UnityEngine;

public class IdleState : IState
{
    private Enemy _enemy;

    public IdleState(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    public void Enter()
    {
        Debug.Log("Idle Enter");
    }

    public void Execute()
    {
        if (_enemy.GetTargetDistance() <= _enemy.Stat.DetectRange)
        {
            _enemy.StateMachine.ChangeState(_enemy.ChaseState);
            return;
        }
    }

    public void Exit()
    {
        Debug.Log("Idle Exit");
    }
}
