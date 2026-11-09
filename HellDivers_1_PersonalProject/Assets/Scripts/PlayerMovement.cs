using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector2 _mousePosInput;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rayDistance = 50f;
    [SerializeField] private LayerMask _groundLayer;
    
    private void Update()
    {
        Move();
        MousePosRay();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    
    private void Move()
    {
        Vector3 moveDir = new Vector3(_moveInput.x, 0f, _moveInput.y);
        moveDir.Normalize();
        
        transform.Translate(moveDir * _moveSpeed * Time.deltaTime, Space.World);
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
}
