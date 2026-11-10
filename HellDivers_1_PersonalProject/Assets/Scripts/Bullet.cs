using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float _speed;
    private float _coolTime;
    
    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        CountLifeTime();
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
