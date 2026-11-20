using UnityEngine;

public class Bomb : BeaconLanding, IBeacon
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
        Debug.Log("폭발");
        Destroy(gameObject);
        // 트리거
        // 범위 내 데미지
        // 폭파 이펙트
    }
}
