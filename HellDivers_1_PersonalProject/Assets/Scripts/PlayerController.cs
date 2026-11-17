using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private WeaponManager _weaponManager;
    
    private int _currentHp;

    private void Awake()
    {
        Init();
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
        BindInputEvents();
    }

    private void Update()
    {
        // CheckDie();
        
        if (_playerMovement.IsStratagem) return;
        
        WeaponManager.Instance.Reload();
        WeaponManager.Instance.OnShoot(transform);
        WeaponManager.Instance.OnChangeGun();
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
    }
}
