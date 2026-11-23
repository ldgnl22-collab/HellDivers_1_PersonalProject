using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] private EnemyData _stat;

    [SerializeField] protected int _currentHp;
    [SerializeField] private Vector3 _spawnPos;
    
    [SerializeField] private Transform _target;
    [SerializeField] protected Animator _animator;
    
    public EnemyData Stat { get { return _stat; } }
    public Vector3 SpawnPos { get { return _spawnPos; } }
    public Transform Target { get { return _target; } }
    
    public StateMachine StateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public AttackState AttackState { get; private set; }
    public ReturnState ReturnState { get; private set; }
    public DeadState DeadState { get; private set; }
    
    protected bool _isDead = false;
    
    private void Awake()
    {
        CreateState();
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        ResetStat();
        SetPlayerPos();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CheckDead();
        StateMachine.Execute();
    }

    public void TakeFromPool()
    {
    }

    public void ReturnToPool()
    {
        ResetStat();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHp <= 0)
        {
            return;
        }
        _currentHp -= damage;
        Debug.Log($"Enemy HP : {_currentHp}");
    }

    public float GetTargetDistance()
    {
        return Vector3.Distance(_target.position, transform.position);
    }
    
    public void ResetSpawnPosition(Vector3 target)
    {
        _spawnPos = target;
        transform.position = _spawnPos;
    }

    private void CheckDead()
    {
        if (_currentHp > 0 || StateMachine.CurrentState == DeadState)
        {
            return;
        }
        
        StateMachine.ChangeState(DeadState);
        MissionManager.Instance.UpdateMissionProgress(MissionType.CountUp, 1);
        GameManager.Instance.EnemyKillCount(1);
        _isDead = true;
    }
    
    public void SetAnim(Vector3 dir)
    {
        if (dir.magnitude > 0)
        {
            _animator.SetLayerWeight(1, 1);
        }
        else
        {
            _animator.SetLayerWeight(1, 0);
        }
        
        _animator.SetFloat("hor", dir.x);
        _animator.SetFloat("ver", dir.y);
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
    
    private void ResetStat()
    {
        _currentHp = _stat.MaxHp;
        _isDead = false;
    }

    private void SetPlayerPos()
    {
        _target = PlayerController.Instance.transform;
    }
}
