using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float _speed;
    private float _coolTime;
    
    private void OnEnable()
    {
        WeaponManager.Instance.OnSpeedChange += SetSpeed;
    }
    
    private void Update()
    {
        Fire();
        CountLifeTime();
    }

    private void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Fire()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void CountLifeTime()
    {
        _coolTime += Time.deltaTime;
        
        if (_coolTime >= 3f)
        {
            PoolManager.Instance.BulletPool.ReturnPool(this);
            _coolTime = 0;
        }
    }

    public void TakeFromPool()
    {
    }

    public void ReturnToPool()
    {
    }
}
