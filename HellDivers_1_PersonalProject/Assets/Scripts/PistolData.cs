using System;
using UnityEngine;

[Serializable]
public class PistolData
{
    private float _bulletSpeed = 10f;
    private float _fireRate = 1f;
    private int _maxMagazineSize = 12;
    private int _remainingBullets;
    
    public float BulletSpeed => _bulletSpeed;
    public float FireRate => _fireRate;
    public int MaxMagazineSize => _maxMagazineSize;
    public int RemainingBullets => _remainingBullets;
}
