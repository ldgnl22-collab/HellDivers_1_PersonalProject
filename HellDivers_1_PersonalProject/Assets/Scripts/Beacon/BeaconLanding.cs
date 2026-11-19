using UnityEngine;

public class BeaconLanding : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    private bool _isInstall;
    
    private void Update()
    {
        if (_isInstall)
        {
            return;
        }
        transform.position += new Vector3(0,-5f * Time.deltaTime,0);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer) == _groundLayer.value)
        {
            _isInstall = true;
        }
    }
}
