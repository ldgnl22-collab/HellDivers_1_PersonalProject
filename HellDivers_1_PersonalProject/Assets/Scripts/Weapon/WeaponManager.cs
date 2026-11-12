using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private InputActionReference _fireInput;
    [SerializeField] private InputActionReference _changeInput;
    [SerializeField] private InputActionReference _reloadInput;
    
    private float _lastFireTime;
    private float _reloadTime;

    private List<GunData> _guns = new();
    private int _gunIndex;

    private GunData _currentGun;

    public GunData CurrentGun
    {
        get { return _currentGun; }
    }

    private GunData _pistolData;
    private GunData _rifleData;
    private GunData _shotgunData;

    [SerializeField] private Transform _gunPos;
    
    [SerializeField] private GameObject _pistolPrefab;
    [SerializeField] private GameObject _riflePrefab;
    [SerializeField] private GameObject _shotgunPrefab;
    
    public static WeaponManager Instance;
    
    public event Action<float> OnSpeedChange;
    // public event Action<float> OnSpreadChange;
    private Coroutine _reloadRoutine;

    private void Awake()
    {
        Init();
        SetSingleton();
    }
    
    private void OnEnable()
    {
        _fireInput.action.Enable();
        _changeInput.action.Enable();
        _reloadInput.action.Enable();
    }

    private void OnDisable()
    {
        _fireInput.action.Disable();
        _changeInput.action.Disable();
        _reloadInput.action.Disable();
    }

    public void OnChangeGun()
    {
        if (_changeInput.action.WasPressedThisFrame())
        {
            if (_reloadRoutine != null)
            {
                StopCoroutine(_reloadRoutine);
                _reloadRoutine = null;
                Debug.Log($"{_currentGun.RemainingBullets} / {_currentGun.MaxMagazineSize} 장전 안됨");
            }
            
            float inputScroll = _changeInput.action.ReadValue<float>();

            if (inputScroll > 0 && _gunIndex >= 0)
            {
                if (_gunIndex == 0)
                {
                    _gunIndex = _guns.Count - 1;
                    _currentGun = _guns[_gunIndex];
                    SetActiveGun();
                    Debug.Log($"{_currentGun.Name} {_currentGun.RemainingBullets} / {_currentGun.MaxMagazineSize}");
                    OnSpeedChange.Invoke(_guns[_gunIndex].BulletSpeed);
                    return;
                }
                _gunIndex--;
            }
            else if (inputScroll < 0 && _gunIndex < _guns.Count)
            {
                if (_gunIndex == _guns.Count-1)
                {
                    _gunIndex = 0;
                    _currentGun = _guns[_gunIndex];
                    SetActiveGun();
                    Debug.Log($"{_currentGun.Name} {_currentGun.RemainingBullets} / {_currentGun.MaxMagazineSize}");
                    OnSpeedChange.Invoke(_guns[_gunIndex].BulletSpeed);
                    return;
                }
                _gunIndex++;
            }

            _currentGun = _guns[_gunIndex];
            SetActiveGun();
            Debug.Log($"{_currentGun.Name} {_currentGun.RemainingBullets} / {_currentGun.MaxMagazineSize}");
            OnSpeedChange.Invoke(_guns[_gunIndex].BulletSpeed);
        }
    }

    public void OnShoot(Transform startPos)
    {
        if (_reloadRoutine != null)
        {
            return;
        }
        
        if (_fireInput.action.IsPressed())
        {
            if (_currentGun.RemainingBullets <= 0)
            {
                Debug.Log("총알 없음");
                return;
            }
            
            if (Time.time >= _lastFireTime + _currentGun.FireRate)
            {
                if (_currentGun.Name.Contains("ShotGun"))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        PoolManager.Instance.BulletPool.TakePool();
                    }
                }
                else
                {
                    PoolManager.Instance.BulletPool.TakePool();
                }
                
                _currentGun.SetRemainingBullets(_currentGun.RemainingBullets - 1);
                Debug.Log($"{_currentGun.Name} 탄약 수 : {_currentGun.RemainingBullets} / {_currentGun.MaxMagazineSize}");
                
                _lastFireTime = Time.time;
            }
        }
    }
    
    public void Reload()
    {
        if (_currentGun.RemainingBullets >= _currentGun.MaxMagazineSize)
        {
            return;
        }
        
        if (_reloadInput.action.WasPressedThisFrame())
        {
            _reloadRoutine = StartCoroutine(ReloadRoutine());
        }
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(_currentGun.ReloadTime);
        
        _currentGun.SetRemainingBullets(_currentGun.MaxMagazineSize);
        Debug.Log("장전 완료");
        
        StopCoroutine(ReloadRoutine());
        _reloadRoutine = null;
    }

    private void Init()
    {
        GameObject pistol = Instantiate(_pistolPrefab);
        GameObject rifle = Instantiate(_riflePrefab);
        GameObject shotgun = Instantiate(_shotgunPrefab);
        
        _pistolData = new GunData(pistol, "Pistol", 10, 0.5f, 0.7f, 12, 10f);
        _rifleData = new GunData(rifle, "Rifle", 15, 0.1f, 1.1f, 30, 15f);
        _shotgunData = new GunData(shotgun, "ShotGun", 20, 1f, 1.5f, 8, 40f);

        _guns.Add(_pistolData);
        _guns.Add(_rifleData);
        _guns.Add(_shotgunData);

        _gunIndex = 0;
        _currentGun = _guns[_gunIndex];
        
        SetGunReady();
    }

    private void SetGunReady()
    {
        for (int i = 0; i < _guns.Count; i++)
        {
            _guns[i].SetPrefabPosition(_gunPos);
            _guns[i].Prefab.SetActive(false);
        }
        
        SetActiveGun();
    }

    private void SetActiveGun()
    {
        for (int i = 0; i < _guns.Count; i++)
        {
            if (_guns[i].Name == _currentGun.Name)
            {
                _guns[i].Prefab.SetActive(true);
            }
            else
            {
                _guns[i].Prefab.SetActive(false);
            }
        }
    }

    private void SetSingleton()
    {
        if (Instance != null &&  Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
