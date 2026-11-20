using UnityEngine;

public class Beacon : MonoBehaviour, IPoolable
{
    private Rigidbody _rb;
    private bool _isHoldPos;
    
    [SerializeField] private LayerMask _groundLayer;

    private float _activeTime = 2f;
    private float _elapsedTime = 0f;
    
    public bool IsHoldPos => _isHoldPos;

    private bool _isGround;

    private bool _isLandingMode;
    private GameObject _beaconMode;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _isHoldPos = true;
    }

    private void Update()
    {
        ActiveTimer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer) == _groundLayer.value)
        {
            _isGround = true;
        }
    }

    private void ActiveTimer()
    {
        if (_isGround)
        {
            _elapsedTime += Time.deltaTime;
        
            if (_elapsedTime >= _activeTime)
            {
                PoolManager.Instance.BeaconPool.ReturnPool(this);
                _elapsedTime = 0f;
                _isGround = false;
            }
        }
    }

    public void Throw()
    {
        _isHoldPos = false;
        _rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }

    public void TakeFromPool()
    {
    }

    public void ReturnToPool()
    {
        BeaconSystem.Instance.SetBeacon();
    }
}
