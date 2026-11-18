using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StratagemSystem : MonoBehaviour
{
    private List<int> _stratagem = new();
    
    [SerializeField] private InputActionReference _wKey;
    [SerializeField] private InputActionReference _aKey;
    [SerializeField] private InputActionReference _sKey;
    [SerializeField] private InputActionReference _dKey;

    private float _elapsed;

    private void Start()
    {
        Init();
    }
    
    private void OnEnable()
    {
        _wKey.action.Enable();
        _aKey.action.Enable();
        _sKey.action.Enable();
        _dKey.action.Enable();
    }

    private void Update()
    {
        InputDelayStratagem(2f);
    }
    
    private void OnDisable()
    {
        _wKey.action.Disable();
        _aKey.action.Disable();
        _sKey.action.Disable();
        _dKey.action.Disable();
    }

    public void InputStratagem()
    {
        if (_wKey.action.WasPressedThisFrame())
        {
            _stratagem.Add((int)ArrowType.Up);
            _elapsed = 0f;
        }
        else if (_aKey.action.WasPressedThisFrame())
        {
            _stratagem.Add((int)ArrowType.Left);
            _elapsed = 0f;
        }
        else if (_sKey.action.WasPressedThisFrame())
        {
            _stratagem.Add((int)ArrowType.Down);
            _elapsed = 0f;
        }
        else if (_dKey.action.WasPressedThisFrame())
        {
            _stratagem.Add((int)ArrowType.Right);
            _elapsed = 0f;
        }
    }

    private void InputDelayStratagem(float delayTime)
    {
        _elapsed += Time.deltaTime;
        
        if (_elapsed >= delayTime)
        {
            ResetStratagem();
        }
    }

    private void ResetStratagem()
    {
        _stratagem.Clear();
        _elapsed = 0f;
        // UI 로 전달
    }

    private void Init()
    {
        ResetStratagem();
    }
}
