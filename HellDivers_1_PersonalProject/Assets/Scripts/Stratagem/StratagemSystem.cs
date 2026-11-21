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
    public List<StratagemData> ListStratagemData => _listStratagemData;
    public List<StratagemData> ListCheckStratagem => _listCheckStratagem;
    
    private BeaconSystem _beaconSystem;

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
        List<StratagemData> tempList = new();
        for (int i = 0; i < _listCheckStratagem.Count; i++)
        {
            if (_listCheckStratagem[i].Stratagem.Count <= index)
            {
                continue;
            }
            if (_listCheckStratagem[i].Stratagem[index] == _listInputStratagem[index])
            {
                tempList.Add(_listCheckStratagem[i]);
            }
        }
        _listCheckStratagem.Clear();
        _listCheckStratagem = new(tempList);
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
        CallStratagem();
        
        _listInputStratagem.Clear();
        _elapsed = 0f;
        // 데이터 리스트 -> 체크용 리스트에 복사
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
        _beaconSystem = GetComponent<BeaconSystem>();
    }
}
