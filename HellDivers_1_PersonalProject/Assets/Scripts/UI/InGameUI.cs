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

    [SerializeField] private List<GameObject> _listArrowPanel;

    [SerializeField] private Sprite _arrowUp;
    [SerializeField] private Sprite _arrowDown;
    [SerializeField] private Sprite _arrowLeft;
    [SerializeField] private Sprite _arrowRight;
    
    [SerializeField] private GameObject _finalStratagemPanel;
    
    private void Start()
    {
        PlayerController.Instance.OnPlayerHpChanged += SetHpBar;
        WeaponManager.Instance.OnWeaponChecked += SetWeaponInfo;
        BeaconSystem.Instance.OnBeaconChecked += SetBeaconInfo;
        MissionManager.Instance.OnMissionUpdate += SetMissionInfo;
        StratagemSystem.Instance.OnBeaconDataChanged += SetArrowPanel;
        StratagemSystem.Instance.OnStratagemInputChanged += CheckInputArrow;
        StratagemSystem.Instance.OnFinalMissionActive += SetFinalArrow;
    }

    private void OnDisable()
    {
        PlayerController.Instance.OnPlayerHpChanged -= SetHpBar;
        WeaponManager.Instance.OnWeaponChecked -= SetWeaponInfo;
        BeaconSystem.Instance.OnBeaconChecked -= SetBeaconInfo;
        MissionManager.Instance.OnMissionUpdate -= SetMissionInfo;
        StratagemSystem.Instance.OnBeaconDataChanged -= SetArrowPanel;
        StratagemSystem.Instance.OnStratagemInputChanged -= CheckInputArrow;
        StratagemSystem.Instance.OnFinalMissionActive -= SetFinalArrow;
    }

    private void SetFinalArrow(StratagemData data)
    {
        for (int i = 0; i < 6; i++)
        {
            Image chlid = _finalStratagemPanel.transform.GetChild(i).gameObject.GetComponent<Image>();
                
            if (data.Stratagem[i] == ArrowType.Down)
            {
                chlid.sprite = _arrowDown;
            }
            else if (data.Stratagem[i] == ArrowType.Up)
            {
                chlid.sprite = _arrowUp;
            }
            else if (data.Stratagem[i] == ArrowType.Left)
            {
                chlid.sprite = _arrowLeft;
            }
            else if (data.Stratagem[i] == ArrowType.Right)
            {
                chlid.sprite = _arrowRight;
            }
        }
    }

    private void SetMissionInfo(List<MissionData> data, List<MissionProgress> progressData)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (progressData[i].IsClear)
            {
                continue;
            }

            if (data[i].MissionType == MissionType.Max)
            {
                MissionManager.Instance.SetFinalMission(true);
            }

            _missionDetail.text = data[i].Detail;
            _missionProgress.text = $"{progressData[i].CurProgress} / {data[i].TargetValue}";
            return;
        }
    }

    private void CheckInputArrow(List<ArrowType> arrow, List<StratagemData> data)
    {
        for (int i = 0; i < _listArrowPanel.Count; i++)
        {
            _listArrowPanel[i].SetActive(true);
        }
        
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < arrow.Count; j++)
            {
                Image spr = _listArrowPanel[i].transform.GetChild(j).GetComponent<Image>();

                if (arrow.Count > data[i].Stratagem.Count)
                {
                    continue;
                }
                if (arrow[j] == data[i].Stratagem[j])
                {
                    spr.color = Color.blue;
                }
            }
        }
    }

    private void SetArrowPanel(List<StratagemData> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < data[i].Stratagem.Count; j++)
            {
                Image spr = _listArrowPanel[i].transform.GetChild(j).GetComponent<Image>();
                spr.color = Color.white;
                
                if (data[i].Stratagem[j] == ArrowType.Down)
                {
                    spr.sprite = _arrowDown;
                }
                else if (data[i].Stratagem[j] == ArrowType.Up)
                {
                    spr.sprite = _arrowUp;
                }
                else if (data[i].Stratagem[j] == ArrowType.Left)
                {
                    spr.sprite = _arrowLeft;
                }
                else if (data[i].Stratagem[j] == ArrowType.Right)
                {
                    spr.sprite = _arrowRight;
                }
            }
        }
        
        for (int i = 0; i < _listArrowPanel.Count; i++)
        {
            _listArrowPanel[i].SetActive(false);
        }
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
