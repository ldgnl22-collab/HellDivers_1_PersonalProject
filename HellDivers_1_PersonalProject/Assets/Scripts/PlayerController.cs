using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController Instance { get; private set; }
    
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    
    [SerializeField] private int _currentHp;

    private void Awake()
    {
        Init();
        SetSingleton();
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
        BindInputEvents();
    }

    private void Update()
    {
        CheckDie();
        
        if (_playerMovement.IsStratagem) return;
        
        WeaponManager.Instance.OnWeapon();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        UnbindInputEvents();
    }

    private void BindInputEvents()
    {
        _playerInput.Stratagem.Stratagem.started += _playerMovement.OnStratagem;
        _playerInput.Stratagem.Stratagem.canceled += _playerMovement.OnStratagem;
        
        _playerInput.Player.Move.performed += _playerMovement.OnMove;
        _playerInput.Player.Move.canceled += _playerMovement.OnMove;

        _playerInput.Player.MousePos.performed += _playerMovement.OnMousePosition;
    }

    private void UnbindInputEvents()
    {
        _playerInput.Stratagem.Stratagem.started -= _playerMovement.OnStratagem;
        _playerInput.Stratagem.Stratagem.canceled -= _playerMovement.OnStratagem;
        
        _playerInput.Player.Move.performed -= _playerMovement.OnMove;
        _playerInput.Player.Move.canceled -= _playerMovement.OnMove;
        
        _playerInput.Player.MousePos.performed -= _playerMovement.OnMousePosition;
    }

    private void CheckDie()
    {
        if (_currentHp > 0)
        {
            return;
        }
        
        _playerInput.Disable();
        UnbindInputEvents();
    }

    public void TakeHeal(int damage)
    {
        if (_currentHp <= 0)
        {
            _currentHp = 0;
            return;
        }
        
        if (_currentHp > 100)
        {
            _currentHp = 100;
        }
        
        Debug.Log($"플레이어 체력 : {_currentHp}");
        _currentHp += damage;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHp <= 0)
        {
            _currentHp = 0;
            return;
        }
        Debug.Log($"플레이어 체력 : {_currentHp}");
        _currentHp -= damage;
    }

    private void Init()
    {
        _playerInput = new PlayerInput();
        
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init();

        _currentHp = 100;
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
