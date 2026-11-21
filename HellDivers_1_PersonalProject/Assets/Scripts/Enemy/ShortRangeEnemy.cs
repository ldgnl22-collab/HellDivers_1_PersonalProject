using System;
using UnityEngine;

public class ShortRangeEnemy : Enemy
{
    private void LateUpdate()
    {
        OnDead();
    }
    
    private void OnDead()
    {
        if (_isDead)
        {
            PoolManager.Instance.ShortEnemy.ReturnPool(this);
        }
    }
}
