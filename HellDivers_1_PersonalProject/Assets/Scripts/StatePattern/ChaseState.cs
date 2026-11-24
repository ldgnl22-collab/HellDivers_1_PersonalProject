using TMPro;
using UnityEngine;

public class ChaseState : IState
{
    private Enemy _enemy;

    public ChaseState(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    public void Enter()
    {
        Debug.Log("Chase Enter");
    }

    public void Execute()
    {
        float dis = _enemy.GetTargetDistance();
        
        if (dis > _enemy.Stat.DetectRange)
        {
            _enemy.StateMachine.ChangeState(_enemy.ReturnState);
            return;
        }
        
        if (dis <= _enemy.Stat.AttackRange && dis >= _enemy.Stat.MinAttackRange)
        {
            _enemy.StateMachine.ChangeState(_enemy.AttackState);
            return;
        }
        
        MoveToTarget();
    }

    public void Exit()
    {
        Debug.Log("Chase Exit");
    }

    private void MoveToTarget()
    {
        Vector3 targetDir = (_enemy.Target.position - _enemy.transform.position).normalized;
        
        _enemy.transform.position += targetDir * _enemy.Stat.MoveSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, targetRotation, 0.3f);
    }
}
