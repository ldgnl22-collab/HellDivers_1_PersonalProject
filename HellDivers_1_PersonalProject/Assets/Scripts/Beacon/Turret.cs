using UnityEngine;

public class Turret : BeaconLanding, IBeacon
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
        Debug.Log("터렛 설치");
        // 트리거
        // 범위 내 타겟 검사
        // 있으면 공격
        // 없으면 순찰 회전
        // ??거리에 따라 데미지 다름??
    }
}
