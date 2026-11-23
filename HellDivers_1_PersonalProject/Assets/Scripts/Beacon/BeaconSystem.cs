using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class BeaconSystem : MonoBehaviour
{
    [SerializeField] private Transform _beaconPos;
    [SerializeField] private GameObject _beaconModePrefab;
    private Beacon _beacon;
    
    private StratagemData _stratagemData;
    
    private BeaconType _beaconType;
    private BeaconUseType _beaconUseType;

    private bool _isOverCount;
    
    private bool _isCoolTime;
    private float _elapsedTime;

    private List<StratagemData> _listUsedCount = new();
    private List<StratagemData> _listCoolTime = new();
    
    private Dictionary<BeaconType, StratagemData> _dictBeacon = new();
    private StratagemSystem _stratagemSystem;
    
    public static BeaconSystem Instance { get; private set; }

    private bool _isBeaconActive;
    public bool IsBeaconActive => _isBeaconActive;
    
    public event Action<Dictionary<BeaconType, StratagemData>> OnBeaconChecked;
    
    private void Awake() => SetSingleton();

    private void Start()
    {
        Init();
        SetBeaconKeys();
    }
    
    private void Update()
    {
        HoldPosition();
        // UseBeacon();
        Timer();
    }

    public void UseBeacon()
    {
        CallBeacon();
        
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
        if (_dictBeacon[_beaconType].UseCount >= _dictBeacon[_beaconType].UseMaxCount)
        {
            return;
        }
        
        _dictBeacon[_beaconType].UseCount++;
        OnBeaconChecked?.Invoke(_dictBeacon);

        _isBeaconActive = true;
        _beacon = PoolManager.Instance.BeaconPool.TakePool();
    }

    private void SetCoolTime()
    {
        if (!_dictBeacon[_beaconType].IsCoolTime)
        {
            _isBeaconActive = true;
            _dictBeacon[_beaconType].IsCoolTime = true;
            _beacon = PoolManager.Instance.BeaconPool.TakePool();
        }
    }

    private void Timer()
    {
        if (_dictBeacon == null)
        {
            return;
        }
        
        foreach (BeaconType key in _dictBeacon.Keys)
        {
            if (_dictBeacon[key].IsCoolTime)
            {
                if (_dictBeacon[key].ElapsedTime >= _dictBeacon[key].CoolTime)
                {
                    _dictBeacon[key].IsCoolTime = false;
                    _dictBeacon[key].ElapsedTime = 0f;
                }
                else
                {
                    _dictBeacon[key].ElapsedTime += Time.deltaTime;
                }
                OnBeaconChecked?.Invoke(_dictBeacon);
            }
        }
    }

    public void SetBeaconData(StratagemData stratagemData)
    {
        _stratagemData = stratagemData;
        _beaconType = stratagemData.BeaconType;
        _beaconUseType = stratagemData.UseType;
        
        Debug.Log($"{_stratagemData.Name} 호출");
    }

    private void CallBeacon()
    {
        if (_stratagemData == null)
        {
            return;
        }
        
        if (_isBeaconActive)
        {
            return;
        }
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

    private void SetBeaconKeys()
    {
        foreach (StratagemData key in _stratagemSystem.ListStratagemData)
        {
            _dictBeacon.Add(key.BeaconType, key);
        }

        foreach (StratagemData key in _dictBeacon.Values)
        {
            _dictBeacon[key.BeaconType].ElapsedTime = 0f;
            _dictBeacon[key.BeaconType].UseCount = 0;
            OnBeaconChecked?.Invoke(_dictBeacon);
        }
    }

    private void Init()
    {
        _stratagemSystem = GetComponent<StratagemSystem>();
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
