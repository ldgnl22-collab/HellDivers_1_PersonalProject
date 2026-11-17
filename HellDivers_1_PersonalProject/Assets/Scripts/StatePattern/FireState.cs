using UnityEngine;

public class FireState : IState
{
    private Enemy _enemy;

    public FireState(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    public void Enter()
    {
    }

    public void Execute()
    {
        float dis = _enemy.GetTargetDistance();
        
        if (dis <= _enemy.Stat.AttackRange)
        {
            // _enemy.StateMachine.ChangeState(_enemy.ChaseState);
            return;
        }

        // BackStep();
    }

    public void Exit()
    {
    }

    private void BackStep()
    {
        Vector3 targetDir = (_enemy.transform.position - _enemy.Target.position).normalized;
        
        _enemy.transform.position += targetDir * _enemy.Stat.MoveSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, targetRotation, 0.3f);
    }
}
