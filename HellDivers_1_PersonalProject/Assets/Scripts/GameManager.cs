using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MissionData _mission;
    private MissionProgress _missionProgress;

    private void Start()
    {
        MissionManager.Instance.OnMissionUpdate += MissionUpdate;
        
        _mission = MissionManager.Instance.GetMissionData(0);
        _missionProgress = MissionManager.Instance.GetMissionProgress(0);
    }

    private void MissionUpdate()
    {
        Debug.Log($"{_mission.Detail}" +
                  $" 진행도 {_missionProgress.CurProgress} / {_mission.TargetValue}");
    }
}
