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
    
    [SerializeField] private int _useCount;
    [SerializeField] private float _coolTime;
    
    public string Name => _name;
    public List<ArrowType> Stratagem => _stratagem;
    
    public GameObject BeaconPrefab => _beaconPrefab;
    
    public BeaconType BeaconType => _beaconType;
    public BeaconUseType UseType => _useType;
    
    public int UseCount => _useCount;
    public float CoolTime => _coolTime;
}
