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

    [SerializeField] private List<StratagemData> _listCheckStratagem = new();

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
        _listCheckStratagem.Clear();
        
        for (int i = 0; i < _listStratagemData.Count; i++)
        {
            if (index >= _listStratagemData[i].Stratagem.Count
                || _listStratagemData[i].Stratagem[index] != _listInputStratagem[index])
            {
                Debug.Log($"{_listStratagemData[i].Name} 스트라타잼 실패");
                continue;
            }
            
            _listCheckStratagem.Add(_listStratagemData[i]);
            
            if ((index + 1) == _listStratagemData[i].Stratagem.Count)
            {
                Debug.Log($"{_listStratagemData[i].Name} 스트라타잼 성공");
            }
        }
    }
    
    public void ResetStratagem()
    {
        _listInputStratagem.Clear();
        _elapsed = 0f;
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
