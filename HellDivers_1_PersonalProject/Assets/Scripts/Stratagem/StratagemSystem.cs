using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StratagemSystem : MonoBehaviour
{
    [SerializeField] private List<ArrowType> _listInputStratagem = new();
    public List<ArrowType> ListInputStratagem => _listInputStratagem;
    
    [SerializeField] private InputActionReference _wKey;
    [SerializeField] private InputActionReference _aKey;
    [SerializeField] private InputActionReference _sKey;
    [SerializeField] private InputActionReference _dKey;

    private float _elapsed;

    [SerializeField] private List<StratagemData> _listStratagemData = new();

    [SerializeField] private List<StratagemData> _listCheckStratagem;

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
            InputKeyDown(ArrowType.Up);
        }
        else if (_aKey.action.WasPressedThisFrame())
        {
            InputKeyDown(ArrowType.Left);
        }
        else if (_sKey.action.WasPressedThisFrame())
        {
            InputKeyDown(ArrowType.Down);
        }
        else if (_dKey.action.WasPressedThisFrame())
        {
            InputKeyDown(ArrowType.Right);
        }
    }

    private void CheckStratagem(int index)
    {
        for (int i = 0; i < _listCheckStratagem.Count; i++)
        {
            if (_listCheckStratagem[i].Stratagem[index] != _listInputStratagem[i])
            {
                _listCheckStratagem.RemoveAt(i);
            }
        }
    }

    private void CallStratagem()
    {
    }
    
    public void ResetStratagem()
    {
        _listInputStratagem.Clear();
        _elapsed = 0f;
        _listCheckStratagem = new(_listStratagemData);
        // UI 로 전달
    }

    private void InputKeyDown(ArrowType arrowType)
    {
        _listInputStratagem.Add(arrowType);
        _elapsed = 0f;
        CheckStratagem(_listInputStratagem.Count - 1);
    }

    private void InputDelayStratagem(float delayTime)
    {
        _elapsed += Time.deltaTime;
        
        if (_elapsed >= delayTime)
        {
            ResetStratagem();
        }
    }

    private void Init()
    {
        ResetStratagem();
    }
}
