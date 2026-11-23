using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool _isClear;
    public bool IsClear => _isClear;

    private int _totalKill;
    public int TotalKill => _totalKill;

    private void Awake() => SetSingleton();

    public void EnemyKillCount(int count)
    {
        _totalKill += count;
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
