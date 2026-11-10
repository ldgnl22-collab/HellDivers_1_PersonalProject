using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private GunData _pistolData;
    private GunData _rifleData;
    private GunData _shotgunData;
    
    public static WeaponManager Instance;
    
    public event Action<float> OnSpeedChange;

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
            float inputScroll = _changeInput.action.ReadValue<float>();

            if (inputScroll > 0 && _gunIndex >= 0)
            {
                if (_gunIndex == 0)
                {
                    _gunIndex = _guns.Count - 1;
                    Debug.Log($"{_gunIndex}");
                    _currentGun = _guns[_gunIndex];
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
                    Debug.Log($"{_gunIndex}");
                    _currentGun = _guns[_gunIndex];
                    OnSpeedChange.Invoke(_guns[_gunIndex].BulletSpeed);
                    return;
                }
                _gunIndex++;
            }
        
            Debug.Log($"{_gunIndex}");
            _currentGun = _guns[_gunIndex];
            OnSpeedChange.Invoke(_guns[_gunIndex].BulletSpeed);
        }
    }

    public void OnShoot(Transform startPos)
    {
        if (_fireInput.action.IsPressed())
        {
            if (_currentGun.RemainingBullets <= 0)
            {
                Debug.Log("총알 없음");
                return;
            }
            
            if (Time.time >= _lastFireTime + _currentGun.FireRate)
            {
                Bullet bullet = PoolManager.Instance.BulletPool.TakePool();
                _currentGun.SetRemainingBullets(_currentGun.RemainingBullets - 1);
                Debug.Log($"탄약 수 : {_currentGun.RemainingBullets} / {_currentGun.MaxMagazineSize}");
                
                bullet.transform.position = startPos.position;
                bullet.transform.rotation = startPos.rotation;
                _lastFireTime = Time.time;
            }
        }
    }
    
    public void Reload()
    {
        if (_reloadInput.action.WasPressedThisFrame())
        {
            _currentGun.SetRemainingBullets(_currentGun.MaxMagazineSize);
            Debug.Log("장전 완료");
            _reloadTime = 0;
        }
    }

    private void Init()
    {
        _pistolData = new GunData(10, 0.5f, 0.7f, 12);
        _rifleData = new GunData(15, 0.1f, 1.1f, 30);
        _shotgunData = new GunData(20, 1f, 1.5f, 8);

        _guns.Add(_pistolData);
        _guns.Add(_rifleData);
        _guns.Add(_shotgunData);

        _gunIndex = 0;
        _currentGun = _guns[_gunIndex];
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
