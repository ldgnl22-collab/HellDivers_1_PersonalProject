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
    
    private float _lastFireTime;

    private List<GunData> _guns = new();
    private int _gunIndex;

    private GunData _currentGun;

    private GunData _pistolData;
    private GunData _RifleData;
    private GunData _shotgunData;

    private void Awake()
    {
        Init();
    }
    
    private void OnEnable()
    {
        _fireInput.action.Enable();
        _changeInput.action.Enable();
    }

    private void OnDisable()
    {
        _fireInput.action.Disable();
        _changeInput.action.Disable();
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
                    return;
                }
                _gunIndex++;
            }
        
            Debug.Log($"{_gunIndex}");
            _currentGun = _guns[_gunIndex];
        }
    }

    public void OnShoot(Transform startPos)
    {
        if (_fireInput.action.IsPressed())
        {
            if (Time.time >= _lastFireTime + _currentGun.FireRate)
            {
                Bullet bullet = PoolManager.Instance.BulletPool.TakePool();
                bullet.transform.position = startPos.position;
                bullet.transform.rotation = startPos.rotation;
                _lastFireTime = Time.time;
            }
        }
    }

    private void Init()
    {
        _pistolData = new GunData(10, 0.5f, 12);
        _RifleData = new GunData(15, 0.1f, 30);
        _shotgunData = new GunData(20, 1f, 8);

        _guns.Add(_pistolData);
        _guns.Add(_RifleData);
        _guns.Add(_shotgunData);

        _gunIndex = 0;
        _currentGun = _guns[_gunIndex];
    }
}
