using UnityEngine;

public class Supply : BeaconLanding, IBeacon
{
    private void OnEnable()
    {
        OnUsingBeacon += UseBeacon;
    }
    
    private void OnDisable()
    {
        OnUsingBeacon -= UseBeacon;
    }
    
    public void UseBeacon()
    {
        if (IsPlayerEnter)
        {
            Debug.Log("지원품 획득");
            Destroy(gameObject);
        }
    }
}
