
public class MissionProgress
{
    private int _missionId;
    private int _curProgress;
    private bool _isClear;

    public int MissionId => _missionId;
    public int CurProgress => _curProgress;
    public bool IsClear => _isClear;

    public MissionProgress(int missionId)
    {
        _missionId = missionId;
    }
    
    public void AddProgress(MissionData missionData, int progress)
    {
        if (_isClear)
        {
            return;
        }
        
        _curProgress += progress;
        if (_curProgress >= missionData.TargetValue)
        {
            _curProgress = missionData.TargetValue;
            _isClear = true;
        }
    }

    public void ResetProgress()
    {
        _curProgress = 0;
        _isClear = false;
    }
}