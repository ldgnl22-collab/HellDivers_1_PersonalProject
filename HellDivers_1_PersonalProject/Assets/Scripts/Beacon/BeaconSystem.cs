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
    
    public static BeaconSystem Instance { get; private set; }

    private bool _isBeaconActive;
    public bool IsBeaconActive => _isBeaconActive;
    
    private void Awake() => SetSingleton();
    
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
        foreach (BeaconType key in _dictBeacon.Keys)
        {
            if (_dictBeacon[key].IsCoolTime || _dictBeacon[key].IsUseCount)
            {
                continue;
            }
            
            if (_dictBeacon[key].UseType == BeaconUseType.UseCount)
            {
                _dictBeacon[key].UseCount++;
            }
        }

        _isBeaconActive = true;
        _beacon = PoolManager.Instance.BeaconPool.TakePool();
        
        foreach (BeaconType key in _dictBeacon.Keys)
        {
            if (_dictBeacon[key].UseCount >= _dictBeacon[key].UseMaxCount)
            {
                _dictBeacon[key].IsUseCount = true;
            }
        }
    }

    private void SetCoolTime()
    {
        foreach (BeaconType key in _dictBeacon.Keys)
        {
            if (_dictBeacon[key].IsCoolTime)
            {
                continue;
            }

            if (!_dictBeacon[key].IsCoolTime)
            {
                _dictBeacon[key].IsCoolTime = true;
                _dictBeacon[key].ElapsedTime = 0f;
            }
        }

        _isBeaconActive = true;
        _beacon = PoolManager.Instance.BeaconPool.TakePool();
    }

    private void Timer()
    {
        if (_dictBeacon == null)
        {
            return;
        }
        foreach (BeaconType key in _dictBeacon.Keys)
        {
            // if (_dictBeacon[key].IsCoolTime)
            // {
            //     Debug.Log($"{_dictBeacon[key].Name} 쿨타임..");
            //     _dictBeacon[key].ElapsedTime += Time.deltaTime;
            //     _dictBeacon[key].IsCoolTime = true;
            // }
            //
            // if (_dictBeacon[key].ElapsedTime >= _dictBeacon[key].CoolTime)
            // {
            //     _dictBeacon[key].IsCoolTime = false;
            //     _dictBeacon[key].ElapsedTime = 0f;
            // }

            if (_dictBeacon[key].ElapsedTime >= _dictBeacon[key].CoolTime)
            {
                _dictBeacon[key].IsCoolTime = false;
                _dictBeacon[key].ElapsedTime = 0f;
                continue;
            }
            
            _dictBeacon[key].IsCoolTime = true;
            _dictBeacon[key].ElapsedTime += Time.deltaTime;
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
        
        if (_dictBeacon.ContainsKey(_beaconType))
        {
            if (_dictBeacon[_beaconType].IsCoolTime)
            {
                return;
            }
            
            _dictBeacon.Remove(_beaconType);
        }

        _dictBeacon.Add(_beaconType, _stratagemData);
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
