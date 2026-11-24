using System;
using UnityEngine;

enum AnimState
{
    Idle,
    Chase,
    Attack,
    Dead
}

public class ShortRangeEnemy : Enemy
{
    private float _elapsed = 0f;
    
    private void OnEnable()
    {
        PlayerController.Instance.OnGameOver += GameOverReturnToPool;
    }
    
    private void LateUpdate()
    {
        OnDead();
        UpdateAnim();
    }
    
    private void GameOverReturnToPool()
    {
        PoolManager.Instance.ShortEnemy.ReturnPool(this);
    }

    private void UpdateAnim()
    {
        if (StateMachine.CurrentState == IdleState)
        {
            _animator.SetInteger("State", (int)AnimState.Idle);
        }
        
        if (StateMachine.CurrentState == ChaseState)
        {
            _animator.SetInteger("State", (int)AnimState.Chase);
        }

        if (StateMachine.CurrentState == AttackState)
        {
            _animator.SetInteger("State", (int)AnimState.Attack);
        }
        
        if (StateMachine.CurrentState == DeadState)
        {
            _animator.SetInteger("State", (int)AnimState.Dead);
        }
    }

    private void SetAttackState()
    {
        
    }
    
    public void OnDead()
    {
        if (_isDead)
        {
            _elapsed += Time.deltaTime;

            if (_elapsed >= 5f)
            {
                _elapsed = 0f;
                PoolManager.Instance.ShortEnemy.ReturnPool(this);
            }
        }
    }
}
