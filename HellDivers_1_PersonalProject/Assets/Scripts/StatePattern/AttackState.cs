using UnityEngine;

public class AttackState : IState
{
    private Enemy _enemy;
    private float _elapsed;

    public AttackState(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    public void Enter()
    {
        _elapsed = 0f;
    }

    public void Execute()
    {
        float dis = _enemy.GetTargetDistance();

        if (dis > _enemy.Stat.AttackRange)
        {
            _enemy.StateMachine.ChangeState(_enemy.ChaseState);
            return;
        }
        
        AttackInterval();
    }

    public void Exit()
    {
        
    }

    private void AttackInterval()
    {
        _elapsed += Time.deltaTime;

        if (_elapsed >= _enemy.Stat.AttackInterval)
        {
            Debug.Log("공격");
            _elapsed = 0f;
        }
        Vector3 targetDir = (_enemy.Target.position - _enemy.transform.position).normalized;
        
        _enemy.SetAnim(targetDir);

        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, targetRotation, 0.3f);
    }
}
