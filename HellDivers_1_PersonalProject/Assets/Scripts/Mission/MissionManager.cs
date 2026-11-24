using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }
    
    // 미션 데이터 및 진행도 데이터 리스트
    [SerializeField] private List<MissionData> _listMissionData = new List<MissionData>();
    private List<MissionProgress> _listMissionProgress =  new List<MissionProgress>();
    
    public event Action<MissionData, MissionProgress> OnMissionUpdate;
    
    private void Awake()
    {
        Init();
        SetSingleton();
    }
    
    public MissionData GetMissionData(int id)
    {
        return _listMissionData[id];
    }
    
    public MissionProgress GetMissionProgress(int id)
    {
        return _listMissionProgress[id];
    }

    // 미션 진행도 추가 ('count'는 1 혹은 누적된 점수 값)
    public void UpdateMissionProgress(MissionType missionType, int count)
    {
        for (int i = 0; i < _listMissionProgress.Count; i++)
        {
            if (_listMissionProgress[i].IsClear||
                missionType != _listMissionData[i].MissionType)
            {
                continue;
            }

            if (missionType == _listMissionData[i].MissionType)
            {
                _listMissionProgress[i].AddProgress(_listMissionData[i], count);
                OnMissionUpdate?.Invoke(_listMissionData[i], _listMissionProgress[i]);

                return;
            }
        }
    }

    // private void InitMissionData()
    // {
    //     MissionData missionData
    //         = new(0, MissionType.CountUp, "Kill 50 Enemies", 50, 1);
    //     
    //     _listMissionData.Add(missionData);
    // }
    
    private void InitProgressData()
    {
        for (int i = 0; i < _listMissionData.Count; i++)
        {
            MissionProgress missionProgress = new MissionProgress(_listMissionData[i].Id);
            _listMissionProgress.Add(missionProgress);
        }
    }

    public void ResetMissionProgress()
    {
        for (int i = 0; i < _listMissionProgress.Count; i++)
        {
            _listMissionProgress[i].ResetProgress();
        }
    }
    
    private void Init()
    {
        // InitMissionData();
        InitProgressData();
        OnMissionUpdate?.Invoke(_listMissionData[0], _listMissionProgress[0]);
    }
    
    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}