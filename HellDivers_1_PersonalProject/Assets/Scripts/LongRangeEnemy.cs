using UnityEngine;

public class LongRangeEnemy : Enemy, IDamageable
{
    public void TakeDamage(int damage)
    {
        if (_currentHp <= 0)
        {
            return;
        }
        _currentHp -= damage;
        Debug.Log($"Enemy HP : {_currentHp}");
    }
}
