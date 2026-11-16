using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour, IPoolable
{
    private float _speed;
    private int _damage;
    private float _coolTime;
    
    private float _spreadAngle;

    [SerializeField] private LayerMask _enemyLayer;
    
    private void OnEnable()
    {
        WeaponManager.Instance.OnSpeedChange += SetSpeed;
        WeaponManager.Instance.OnDamageChange += SetDamage;
    }
    
    private void Update()
    {
        Fire();
        CountLifeTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            GetDamage(damageable);
        }
    }

    private void GetDamage(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);

        PoolManager.Instance.BulletPool.ReturnPool(this);
        _coolTime = 0;
    }

    private void SetDamage(int damage)
    {
        _damage = damage;
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
    
    private Quaternion SetSpreadAngle(GunData currentGun)
    {
        Quaternion baseAngle = currentGun.Prefab.transform.GetChild(1).rotation;
        _spreadAngle = currentGun.SpreadRange;
        
        float angle = Random.Range(-_spreadAngle/2, _spreadAngle/2);

        Quaternion randomAngle = baseAngle * Quaternion.Euler(0, angle, 0);

        return randomAngle;
    }

    public void TakeFromPool()
    {
        GunData currentGun = WeaponManager.Instance.CurrentGun;
        
        transform.position = currentGun.Prefab.transform.GetChild(1).position;
        transform.rotation = SetSpreadAngle(currentGun);
    }

    public void ReturnToPool()
    {
    }
}
