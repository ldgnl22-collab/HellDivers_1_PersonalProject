using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private InputActionReference _fireInput;
    [SerializeField] private GameObject _bullet;

    private PistolData _pistolData;
    
    private float _elapsedTime = 0f;

    private void Awake()
    {
        Init();
    }
    
    private void OnEnable()
    {
        _fireInput.action.Enable();
    }

    private void Update()
    {
    }

    private void OnDisable()
    {
        _fireInput.action.Disable();
    }

    public void OnShoot()
    {
        if (_fireInput.action.IsPressed())
        {
            if (!FireRateTimer(_pistolData.FireRate))
            {
                Instantiate(_bullet, transform.position, transform.rotation);
            }
            else
            {
                _elapsedTime = _pistolData.FireRate;
            }
        }
    }

    private bool FireRateTimer(float time)
    {
        _elapsedTime += Time.deltaTime;
        
        if (_elapsedTime >= time)
        {
            _elapsedTime = 0f;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Init()
    {
        _pistolData = new PistolData();
    }
}
