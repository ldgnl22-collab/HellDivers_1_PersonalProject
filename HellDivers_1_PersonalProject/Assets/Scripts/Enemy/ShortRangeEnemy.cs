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
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private bool _isPlayerDetect;

    private int _attackDamage;
    
    private void OnEnable()
    {
        PlayerController.Instance.OnGameOver += GameOverReturnToPool;
    }
    
    private void LateUpdate()
    {
        OnDead();
        UpdateAnim();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isPlayerDetect && (_playerLayer.value == (1 << other.gameObject.layer)))
        {
            Debug.Log("ShortAttack");
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_attackDamage);
                _isPlayerDetect = false;
            }
        }
    }
    
    private void GameOverReturnToPool()
    {
        if (this == null)
            return;
        PoolManager.Instance.ShortEnemy.ReturnPool(this);
    }

    public void OnAttack()
    {
        _isPlayerDetect = true;
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
