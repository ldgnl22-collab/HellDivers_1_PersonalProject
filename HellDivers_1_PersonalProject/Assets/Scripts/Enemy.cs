using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _stat;

    [SerializeField] protected int _currentHp;
    [SerializeField] private Vector3 _spawnPos;
    
    [SerializeField] private Transform _target;
    public EnemyData Stat { get { return _stat; } }
    public Vector3 SpawnPos { get { return _spawnPos; } }
    public Transform Target { get { return _target; } }
    
    public StateMachine StateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public AttackState AttackState { get; private set; }
    public ReturnState ReturnState { get; private set; }
    public DeadState DeadState { get; private set; }
    
    private void Awake()
    {
        CreateState();
    }
    
    private void Start()
    {
        ResetHp();
        ResetMyPosition();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CheckDead();
        StateMachine.Execute();
    }

    public float GetTargetDistance()
    {
        return Vector3.Distance(_target.position, transform.position);
    }

    private void CheckDead()
    {
        if (_currentHp > 0 || StateMachine.CurrentState == DeadState)
        {
            return;
        }
        
        StateMachine.ChangeState(DeadState);
    }

    private void CreateState()
    {
        StateMachine = new StateMachine();
        IdleState = new IdleState(this);
        ChaseState = new ChaseState(this);
        AttackState = new AttackState(this);
        ReturnState = new ReturnState(this);
        DeadState = new DeadState(this);
    }
    
    private void ResetHp()
    {
        _currentHp = _stat.MaxHp;
    }

    private void ResetMyPosition()
    {
        _spawnPos = transform.position;
    }
}
