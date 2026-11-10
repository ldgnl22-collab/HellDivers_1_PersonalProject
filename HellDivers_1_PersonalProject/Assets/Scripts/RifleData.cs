using System;

[Serializable]
public class RifleData
{
    private float _bulletSpeed = 15f;
    private float _fireRate = 0.1f;
    private int _maxMagazineSize = 30;
    private int _remainingBullets;
    
    public float BulletSpeed => _bulletSpeed;
    public float FireRate => _fireRate;
    public int MaxMagazineSize => _maxMagazineSize;
    public int RemainingBullets => _remainingBullets;
}
