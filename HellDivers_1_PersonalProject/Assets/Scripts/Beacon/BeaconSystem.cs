using UnityEngine;

public class BeaconSystem : MonoBehaviour
{
    [SerializeField] private Transform _beaconPos;
    [SerializeField] private GameObject _beaconModePrefab;
    private Beacon _beacon;
    
    private StratagemData _stratagemData;
    
    private BeaconType _beaconType;
    private BeaconUseType _beaconUseType;

    private bool _isCoolTime;

    private float _elapsedTime;
    
    public static BeaconSystem Instance { get; private set; }

    private bool _isBeaconActive;
    public bool IsBeaconActive => _isBeaconActive;
    
    private void Awake() => SetSingleton();
    
    private void Update()
    {
        HoldPosition();
        UseBeacon();
        Timer();
    }

    private void UseBeacon()
    {
        switch (_beaconUseType)
        {
            case BeaconUseType.UseCount:
                SetUseCount();
                break;
            case BeaconUseType.CoolTime:
                SetCoolTime();
                break;
            default:
                return;
        }
    }

    private void SetUseCount()
    {
    }

    private void SetCoolTime()
    {
        if (_stratagemData.ElapsedTime >= _stratagemData.CoolTime)
        {
            _isCoolTime = false;
        }
        else
        {
            Debug.Log($"{_stratagemData.Name} 쿨타임..");
            _isCoolTime = true;
        }
    }

    private void Timer()
    {
        if (_stratagemData != null && _isCoolTime)
        {
            _stratagemData.ElapsedTime += Time.deltaTime;
        }
    }

    public void SetBeaconData(StratagemData stratagemData)
    {
        _stratagemData = stratagemData;
        _beaconType = stratagemData.BeaconType;
        _beaconUseType = stratagemData.UseType;
        
        Debug.Log($"{_stratagemData.Name} 호출");
    }
    
    public void CallBeacon()
    {
        if (_isCoolTime)
        {
            return;
        }
        
        _stratagemData.ElapsedTime = 0f;
        
        if (_isBeaconActive)
        {
            return;
        }

        _isBeaconActive = true;
        _beacon = PoolManager.Instance.BeaconPool.TakePool();
    }
    
    private void HoldPosition()
    {
        if (_beacon == null || !_beacon.IsHoldPos)
        {
            return;
        }

        if (_beacon.IsHoldPos)
        {
            _beacon.transform.position = _beaconPos.position;
            _beacon.transform.rotation = _beaconPos.rotation;
        }
    }

    public void SetBeacon()
    {
        GameObject go = Instantiate(_stratagemData.BeaconPrefab);
        
        Vector3 pos = new  Vector3(_beacon.transform.position.x, 30f, _beacon.transform.position.z);
        go.transform.position = pos;
    }

    public void ThrowBeacon()
    {
        _beacon.Throw();
        _isBeaconActive = false;
    }

    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
