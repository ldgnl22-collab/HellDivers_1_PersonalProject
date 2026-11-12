using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float _speed;
    private float _coolTime;
    
    private float _spreadAngle;
    
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
