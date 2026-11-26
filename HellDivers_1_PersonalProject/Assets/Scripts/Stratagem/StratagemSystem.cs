using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StratagemSystem : MonoBehaviour
{
    public static StratagemSystem Instance;
    
    [SerializeField] private List<ArrowType> _listInputStratagem = new();
    public List<ArrowType> ListInputStratagem => _listInputStratagem;
    
    [SerializeField] private InputActionReference _wKey;
    [SerializeField] private InputActionReference _aKey;
    [SerializeField] private InputActionReference _sKey;
    [SerializeField] private InputActionReference _dKey;

    private float _elapsed;

    [SerializeField] private List<StratagemData> _listStratagemData = new();

    [SerializeField] private List<StratagemData> _listCheckStratagem;
    public List<StratagemData> ListStratagemData => _listStratagemData;
    public List<StratagemData> ListCheckStratagem => _listCheckStratagem;
    
    [SerializeField] private StratagemData _finalStratagemData;
    public StratagemData FinalStratagemData => _finalStratagemData;
    
    private BeaconSystem _beaconSystem;
    
    public event Action<List<StratagemData>> OnBeaconDataChanged;
    public event Action<List<ArrowType>, List<StratagemData>> OnStratagemInputChanged;
    public event Action<StratagemData> OnFinalMissionActive;

    private void Awake()
    {
        Init();
        SetSingleton();
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
            if (_listCheckStratagem[i].Stratagem.Count <= index)
            {
                continue;
            }
            if (_listCheckStratagem[i].Stratagem[index] != _listInputStratagem[index])
            {
                _listCheckStratagem.RemoveAt(i);
            }
            OnStratagemInputChanged?.Invoke(_listInputStratagem, _listStratagemData);
        }
    }

    private void CallStratagem()
    {
        if (ListCheckStratagem.Count == 1
            && (ListCheckStratagem[0].Stratagem.Count
                == ListInputStratagem.Count))
        {
            _beaconSystem.SetBeaconData(ListCheckStratagem[0]);
            Debug.Log($"스트라타잼 성공");
            _beaconSystem.UseBeacon();
            _listInputStratagem.Clear();
        }
    }
    
    public void ResetStratagem()
    {
        OnBeaconDataChanged?.Invoke(_listStratagemData);
        CallStratagem();
        
        _listInputStratagem.Clear();
        _elapsed = 0f;
        // 데이터 리스트 -> 체크용 리스트에 복사
        _listCheckStratagem = new(_listStratagemData);
        // UI 로 전달
    }

    public void RandomStratagemArrow()
    {
        for (int i = 0; i < 6; i++)
        {
            int rand = Random.Range(0, (int)ArrowType.Max);
            _finalStratagemData.Stratagem.Add((ArrowType)rand);
        }
        
        OnFinalMissionActive?.Invoke(_finalStratagemData);
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
        _beaconSystem = GetComponent<BeaconSystem>();
        
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
