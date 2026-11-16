using UnityEngine;

public class ReturnState : IState
{
    private Enemy _enemy;

    public ReturnState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        float dis = _enemy.GetTargetDistance();
        
        if (dis <= _enemy.Stat.DetectRange)
        {
            _enemy.StateMachine.ChangeState(_enemy.ChaseState);
            return;
        }

        if (Vector3.Distance(_enemy.SpawnPos, _enemy.transform.position) <= 1f)
        {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
            return;
        }
        
        ReturnPos();
    }

    public void Exit()
    {
    }

    private void ReturnPos()
    {
        Vector3 targetDir = (_enemy.SpawnPos - _enemy.transform.position).normalized;
        
        _enemy.transform.position += targetDir * _enemy.Stat.MoveSpeed * Time.deltaTime;
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, targetRotation, 0.3f);
    }
}
