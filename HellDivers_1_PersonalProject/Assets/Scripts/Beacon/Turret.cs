using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Turret : BeaconLanding, IBeacon
{
    [SerializeField] private int _maxMagazineSize = 100;
    [SerializeField] private int _remainingBullets;
    [SerializeField] private float _fireRate = 0.4f;
    [SerializeField] private float _lastFireTime;
    [SerializeField] private float _reloadRate = 3f;

    [SerializeField] private Transform _firePos;
    [SerializeField] private Transform _rotPos;
    
    private void OnEnable()
    {
        OnUsingBeacon += UseBeacon;
    }
    
    private void OnDisable()
    {
        OnUsingBeacon -= UseBeacon;
    }
    
    public void UseBeacon()
    {
        Debug.Log("터렛 설치");
        
        // 범위 내 타겟 검사
        Collider[] bombDamage = Physics.OverlapSphere(transform.position, 10f, _targetDamageLayer);
        
        for (int i = 0; i < bombDamage.Length; i++)
        {
            if (bombDamage[i].TryGetComponent(out Transform tr))
            {
                float dis = Vector3.Distance(transform.position, tr.position);
                
                if (dis >= 0.5f && dis < 10f)
                {
                    Vector3 targetPos = new Vector3(tr.position.x, _rotPos.position.y, tr.position.z);
                    _rotPos.transform.LookAt(targetPos);
                    Fire();
                }
            }
        }
    }

    private void Fire()
    {
        if (_remainingBullets <= 0)
        {
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
            bullet.SetDamage(8);
            bullet.SetSpeed(40);
            bullet.transform.position = _firePos.transform.position;
            bullet.transform.rotation = SetSpreadAngle();

            SetRemainingBullets(_remainingBullets - 1);

            _lastFireTime = Time.time;
        }
    }

    private Quaternion SetSpreadAngle()
    { 
        float spreadAngle;
        
        Quaternion baseAngle = _firePos.transform.rotation;
        spreadAngle = 0.4f;
        
        float angle = Random.Range(-spreadAngle/2, spreadAngle/2);

        Quaternion randomAngle = baseAngle * Quaternion.Euler(0, angle, 0);

        return randomAngle;
    }

    private void SetRemainingBullets(int count)
    {
        _remainingBullets = count;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
