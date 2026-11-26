using UnityEngine;

public class Escape : BeaconLanding, IBeacon
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
            Debug.Log("탈출 비컨 활성화");
            
            StratagemSystem.Instance.RandomStratagemArrow();
        }
    }
}