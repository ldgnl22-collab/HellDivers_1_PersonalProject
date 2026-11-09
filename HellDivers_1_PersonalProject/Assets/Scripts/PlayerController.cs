using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private PlayerMovement _playerMovement;
    private WeaponManager _weaponManager;

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
        _weaponManager.OnShoot();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        UnbindInputEvents();
    }

    private void BindInputEvents()
    {
        _playerInput.Player.Move.performed += _playerMovement.OnMove;
        _playerInput.Player.Move.canceled += _playerMovement.OnMove;

        _playerInput.Player.MousePos.performed += _playerMovement.OnMousePosition;
    }

    private void UnbindInputEvents()
    {
        _playerInput.Player.Move.performed -= _playerMovement.OnMove;
        _playerInput.Player.Move.canceled -= _playerMovement.OnMove;
        
        _playerInput.Player.MousePos.performed -= _playerMovement.OnMousePosition;
    }

    private void Init()
    {
        _playerInput = new PlayerInput();
        
        _playerMovement = GetComponent<PlayerMovement>();
        _weaponManager = GetComponent<WeaponManager>();
    }
}
