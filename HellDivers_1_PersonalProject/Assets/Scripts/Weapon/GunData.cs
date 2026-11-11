using System;

[Serializable]
public class GunData
{
    private string _name;
    private float _bulletSpeed;
    private float _fireRate;
    private float _reloadTime;
    private int _maxMagazineSize;
    private int _remainingBullets;
    
    public string Name => _name;
    public float BulletSpeed => _bulletSpeed;
    public float FireRate => _fireRate;
    public float ReloadTime => _reloadTime;
    public int MaxMagazineSize => _maxMagazineSize;
    public int RemainingBullets => _remainingBullets;

    public GunData(string name, float bulletSpeed, float fireRate, float reloadTime, int maxMagazineSize)
    {
        _name = name;
        _bulletSpeed = bulletSpeed;
        _fireRate = fireRate;
        _reloadTime = reloadTime;
        _maxMagazineSize = maxMagazineSize;
        _remainingBullets = maxMagazineSize;
    }
    
    public void SetRemainingBullets(int remainingBullets)
    {
        _remainingBullets = remainingBullets;
    }
}
