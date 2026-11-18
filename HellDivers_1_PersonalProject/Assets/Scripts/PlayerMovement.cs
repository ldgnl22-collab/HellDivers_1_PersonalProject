using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector2 _mousePosInput;
    
    private StratagemSystem _stratagemSystem;
    
    private bool _isStratagem;
    public bool IsStratagem { get{ return _isStratagem; } }

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rayDistance = 100f;
    [SerializeField] private LayerMask _groundLayer;

    public event Action<Transform> OnPlayerMove;
    
    private void Update()
    {
        Stratagem();
    }

    public void OnStratagem(InputAction.CallbackContext context)
    {
        _isStratagem = context.ReadValueAsButton();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Stratagem()
    {
        if (_isStratagem)
        {
            // Debug.Log("Stratagem");
            _stratagemSystem.InputStratagem();
        }
        else
        {
            if (_stratagemSystem.ListInputStratagem != null)
            {
                _stratagemSystem.ResetStratagem();
            }
            
            Move();
            MousePosRay();
        }
    }
    
    private void Move()
    {
        Vector3 moveDir = new Vector3(_moveInput.x, 0f, _moveInput.y);
        moveDir.Normalize();
        
        transform.Translate(moveDir * _moveSpeed * Time.deltaTime, Space.World);
        OnPlayerMove?.Invoke(transform);
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        _mousePosInput = context.ReadValue<Vector2>();
    }

    private void MousePosRay()
    {
        Vector3 dis = new Vector3(_mousePosInput.x, _mousePosInput.y);
        
        Ray ray = Camera.main.ScreenPointToRay(dis);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _groundLayer))
        {
            Vector3 dir = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            
            transform.LookAt(dir);
        }
    }

    public void Init()
    {
        _stratagemSystem = GetComponent<StratagemSystem>();
    }
}
