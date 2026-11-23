using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMissionData", menuName = "Mission/MissionData")]
public class MissionData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private MissionType _missionType;
    [SerializeField] private string _detail;
    [SerializeField] private int _targetValue;
    [SerializeField] private int _reward;
    
    public int Id => _id;
    public MissionType MissionType => _missionType;
    public string Detail => _detail;
    public int TargetValue => _targetValue;
    public int Reward => _reward;
}