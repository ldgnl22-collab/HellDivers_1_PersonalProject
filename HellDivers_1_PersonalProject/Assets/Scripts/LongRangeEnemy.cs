using UnityEngine;

public class LongRangeEnemy : Enemy, IDamageable
{
    private void LateUpdate()
    {
        KeepFireDistance();
    }
    
    public void TakeDamage(int damage)
    {
        if (_currentHp <= 0)
        {
            return;
        }
        _currentHp -= damage;
        Debug.Log($"Enemy HP : {_currentHp}");
    }

    private void KeepFireDistance()
    {
        if (StateMachine.CurrentState == DeadState)
        {
            return;
        }
        
        float dis = GetTargetDistance();

        if (dis <= Stat.MinAttackRange)
        {
            Vector3 targetDir = (transform.position - Target.position).normalized;

            transform.position += targetDir * (Stat.MoveSpeed - 2) * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(-targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.3f);
        }
    }
}
