using System;

[Serializable]
public class GunData
{
    private float _bulletSpeed;
    private float _fireRate;
    private int _maxMagazineSize;
    private int _remainingBullets;
    
    public float BulletSpeed => _bulletSpeed;
    public float FireRate => _fireRate;
    public int MaxMagazineSize => _maxMagazineSize;
    public int RemainingBullets => _remainingBullets;

    public GunData(float bulletSpeed, float fireRate, int maxMagazineSize)
    {
        _bulletSpeed = bulletSpeed;
        _fireRate = fireRate;
        _maxMagazineSize = maxMagazineSize;
    }
}
