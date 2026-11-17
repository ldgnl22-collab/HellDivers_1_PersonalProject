using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStat", menuName = "Enemy/EnemyStat")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int _maxHp;
    [SerializeField] private float _detectRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _minAttackRange;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackInterval;
    [SerializeField] private string _name;
    
    public int MaxHp => _maxHp;
    public float DetectRange => _detectRange;
    public float AttackRange => _attackRange;
    public float MinAttackRange => _minAttackRange;
    public float MoveSpeed => _moveSpeed;
    public float AttackInterval => _attackInterval;
    public string Name => _name;
}
