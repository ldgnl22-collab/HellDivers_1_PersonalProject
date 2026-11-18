using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStratagemData", menuName = "StratagemSystem/StratagemData")]
public class StratagemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<ArrowType> _stratagem;
    [SerializeField] private int _useCount;
    [SerializeField] private float _coolTime;
    
    public string Name => _name;
    public List<ArrowType> Stratagem => _stratagem;
    public int UseCount => _useCount;
    public float CoolTime => _coolTime;
}
