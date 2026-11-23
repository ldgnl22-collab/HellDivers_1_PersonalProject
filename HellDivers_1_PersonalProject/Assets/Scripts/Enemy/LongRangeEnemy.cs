using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LongRangeEnemy : Enemy
{
    [SerializeField] private int _maxMagazineSize = 25;
    [SerializeField] private int _remainingBullets;
    [SerializeField] private float _fireRate = 0.4f;
    [SerializeField] private float _lastFireTime;
    [SerializeField] private float _reloadRate = 1.7f;
    
    private float _elapsed = 0f;

    private void OnEnable()
    {
        PlayerController.Instance.OnGameOver += GameOverReturnToPool;
    }

    private void LateUpdate()
    {
        OnDead();
        Fire();
        KeepFireDistance();
    }
    
    private void GameOverReturnToPool()
    {
        PoolManager.Instance.LongEnemy.ReturnPool(this);
    }
    
    private void OnDead()
    {
        if (_isDead)
        {
            _animator.SetBool("IsDead", true);
            _elapsed += Time.deltaTime;

            if (_elapsed >= 5f)
            {
                PoolManager.Instance.LongEnemy.ReturnPool(this);
                ResetStat();
            }
        }
    }

    private void ResetStat()
    {
        _maxMagazineSize = 25;
        _remainingBullets = 0;
        _lastFireTime = 0f;
        _elapsed = 0f;
    }

    private void Fire()
    {
        if (StateMachine.CurrentState == AttackState)
        {
            if (_remainingBullets <= 0)
            {
                Debug.Log("총알 없음");
                if (Time.time >= _lastFireTime + _reloadRate)
                {
                    _remainingBullets = _maxMagazineSize;
                    _lastFireTime = Time.time;
                }

                return;
            }

            if (Time.time >= _lastFireTime + _fireRate)
            {
                Bullet bullet = PoolManager.Instance.BulletPool.TakePool();
                bullet.SetIsShootPlayer(false);
                bullet.SetDamage(5);
                bullet.SetSpeed(17);
                bullet.transform.position = transform.GetChild(1).position;
                bullet.transform.rotation = SetSpreadAngle();

                SetRemainingBullets(_remainingBullets - 1);
                Debug.Log($"탄약 수 : {_remainingBullets} / {_maxMagazineSize}");

                _lastFireTime = Time.time;
            }
        }
    }
    
    private Quaternion SetSpreadAngle()
    { 
        float spreadAngle;
        
        Quaternion baseAngle = gameObject.transform.GetChild(1).rotation;
        spreadAngle = 0.4f;
        
        float angle = Random.Range(-spreadAngle/2, spreadAngle/2);

        Quaternion randomAngle = baseAngle * Quaternion.Euler(0, angle, 0);

        return randomAngle;
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
            
            SetAnim(targetDir);

            transform.position += targetDir * (Stat.MoveSpeed - 2) * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(-targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.3f);
        }
    }

    private void SetRemainingBullets(int count)
    {
        _remainingBullets = count;
    }
}
