using UnityEngine;

public class DeadState : IState
{
    private Enemy _enemy;

    public DeadState(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    public void Enter()
    {
        Debug.Log("사망");
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}
