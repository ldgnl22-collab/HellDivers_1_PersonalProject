using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }
    
    // 미션 데이터 및 진행도 데이터 리스트
    private List<MissionData> _listMissionData = new List<MissionData>();
    private List<MissionProgress> _listMissionProgress =  new List<MissionProgress>();
    
    public Action OnMissionUpdate;
    
    private void Awake()
    {
        Init();
        SetSingleton();
    }
    
    // 미션 텍스트 가져가기
    public string GetMissionDetail(int id)
    {
        string detail = string.Format(_listMissionData[id-1].Detail, _listMissionData[id-1].TargetValue);
        return detail;
    }

    // 모든 미션 데이터 가져가기
    public IEnumerable<MissionData> GetMissionDataList()
    {
        foreach (MissionData missionData in _listMissionData)
        {
            yield return missionData;
        }
    }

    // 미션 진행정보 가져가기
    public IEnumerable<MissionProgress> GetMissionProgress()
    {
        foreach (MissionProgress missionProgress in _listMissionProgress)
        {
            yield return missionProgress;
        }
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
                OnMissionUpdate?.Invoke();

                return;
            }
        }
    }

    private void InitMissionData()
    {
        MissionData missionData
            = new(0, MissionType.CountUp, "적 50명 처치", 50, 1);
        
        _listMissionData.Add(missionData);
    }
    
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
        InitMissionData();
        InitProgressData();
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