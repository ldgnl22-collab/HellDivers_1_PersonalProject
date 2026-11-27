using UnityEngine;

public class FinalHost : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_playerLayer.value == (1 << other.gameObject.layer))
        {
            Debug.Log("dfdf");
        }
    }
}
