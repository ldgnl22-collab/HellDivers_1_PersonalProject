using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStratagemData", menuName = "StratagemSystem/StratagemData")]
public class StratagemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<ArrowType> _stratagem;
    
    [SerializeField] private GameObject _beaconPrefab;
    
    [SerializeField] private BeaconType _beaconType;
    [SerializeField] private BeaconUseType _useType;
    
    [SerializeField] private int _useMaxCount;
    [SerializeField] private bool _isUseCount;
    [SerializeField] private int _useCount;
    
    [SerializeField] private float _coolTime;
    [SerializeField] private bool _isCoolTime;
    [SerializeField] private float _elapsedTime;
    
    public string Name => _name;
    public List<ArrowType> Stratagem => _stratagem;
    
    public GameObject BeaconPrefab => _beaconPrefab;
    
    public BeaconType BeaconType => _beaconType;
    public BeaconUseType UseType => _useType;
    
    public int UseMaxCount => _useMaxCount;
    public bool IsUseCount { get => _isUseCount; set => _isUseCount = value; }
    public int UseCount { get => _useCount; set => _useCount = value; }
    public float CoolTime => _coolTime;
    public bool IsCoolTime { get => _isCoolTime; set => _isCoolTime = value; }
    public float ElapsedTime { get => _elapsedTime; set => _elapsedTime = value; }
}
