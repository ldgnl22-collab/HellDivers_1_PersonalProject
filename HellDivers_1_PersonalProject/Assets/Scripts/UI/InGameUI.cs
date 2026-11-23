using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Image _playerHp;
    [SerializeField] private TextMeshProUGUI _WeaponName;
    [SerializeField] private TextMeshProUGUI _WeaponBullet;

    [SerializeField] private TextMeshProUGUI _missionDetail;
    [SerializeField] private TextMeshProUGUI _missionProgress;
    
    [SerializeField] private TextMeshProUGUI _BeaconNameOne;
    [SerializeField] private Image _BeaconCoolTimeOne;
    [SerializeField] private TextMeshProUGUI _BeaconNameTwo;
    [SerializeField] private Image _BeaconCoolTimeTwo;
    [SerializeField] private TextMeshProUGUI _BeaconNameThree;
    [SerializeField] private TextMeshProUGUI _BeaconCountThree;
    
    private void Start()
    {
        PlayerController.Instance.OnPlayerHpChanged += SetHpBar;
        WeaponManager.Instance.OnWeaponChecked += SetWeaponInfo;
        BeaconSystem.Instance.OnBeaconChecked += SetBeaconInfo;
        MissionManager.Instance.OnMissionUpdate += SetMissionInfo;
    }

    private void OnDisable()
    {
        PlayerController.Instance.OnPlayerHpChanged -= SetHpBar;
        WeaponManager.Instance.OnWeaponChecked -= SetWeaponInfo;
        BeaconSystem.Instance.OnBeaconChecked -= SetBeaconInfo;
        MissionManager.Instance.OnMissionUpdate -= SetMissionInfo;
    }

    private void SetMissionInfo(MissionData data, MissionProgress progress)
    {
        _missionDetail.text = data.Detail;
        _missionProgress.text = $"{progress.CurProgress} / {data.TargetValue}";
    }

    private void SetBeaconInfo(Dictionary<BeaconType, StratagemData> data)
    {
        _BeaconNameOne.text = data[BeaconType.Bomb].name;
        _BeaconCoolTimeOne.fillAmount = data[BeaconType.Bomb].ElapsedTime / data[BeaconType.Bomb].CoolTime;
        _BeaconNameTwo.text = data[BeaconType.Supply].name;
        _BeaconCoolTimeTwo.fillAmount = data[BeaconType.Supply].ElapsedTime / data[BeaconType.Supply].CoolTime;
        _BeaconNameThree.text = data[BeaconType.Turret].name;
        _BeaconCountThree.text = $"{data[BeaconType.Turret].UseCount} / {data[BeaconType.Turret].UseMaxCount}";
    }

    private void SetWeaponInfo(GunData gun)
    {
        _WeaponName.text = gun.Name;

        _WeaponBullet.text = $"{gun.RemainingBullets} / {gun.MaxMagazineSize}";
    }

    private void SetHpBar(float hp)
    {
        Debug.Log("데미지");
        _playerHp.fillAmount = hp;
    }
}
