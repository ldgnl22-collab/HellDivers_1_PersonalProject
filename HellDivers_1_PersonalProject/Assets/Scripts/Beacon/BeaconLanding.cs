using System;
using UnityEngine;

public class BeaconLanding : MonoBehaviour
{
    [SerializeField] protected LayerMask _groundLayer;
    [SerializeField] protected LayerMask _playerLayer;

    protected bool IsInstall;
    protected bool IsPlayerEnter;
    
    public event Action OnUsingBeacon;
    
    private void Update()
    {
        if (IsInstall)
        {
            // OnUsingBeacon?.Invoke();
            return;
        }
        transform.position += new Vector3(0,-5f * Time.deltaTime,0);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer) == _groundLayer.value)
        {
            IsInstall = true;
        }

        if ((1 << collision.gameObject.layer) == _playerLayer.value)
        {
            IsPlayerEnter = true;
        }
    }
}
