using UnityEngine;

public class Bomb : BeaconLanding, IBeacon
{
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
        Debug.Log("폭발");
        
        // 범위 내 데미지
        Collider[] bombDamage = Physics.OverlapSphere(transform.position, 10f, _targetDamageLayer);
        
        for (int i = 0; i < bombDamage.Length; i++)
        {
            if (bombDamage[i].TryGetComponent(out IDamageable damageable))
            {
                if (damageable != null)
                {
                    damageable.TakeDamage(50);
                }
            }
        }
        // 폭파 이펙트
        
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
