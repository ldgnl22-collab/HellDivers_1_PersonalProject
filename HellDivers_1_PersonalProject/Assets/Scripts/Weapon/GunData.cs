using System;
using UnityEngine;

[Serializable]
public class GunData
{
    private GameObject _prefab;
    private string _name;
    private float _bulletSpeed;
    private float _fireRate;
    private float _reloadTime;
    private int _maxMagazineSize;
    private int _remainingBullets;
    private float _spreadRange;
    
    public GameObject Prefab => _prefab;
    public string Name => _name;
    public float BulletSpeed => _bulletSpeed;
    public float FireRate => _fireRate;
    public float ReloadTime => _reloadTime;
    public int MaxMagazineSize => _maxMagazineSize;
    public int RemainingBullets => _remainingBullets;
    public float SpreadRange => _spreadRange;

    public GunData(GameObject prefab, string name, float bulletSpeed, float fireRate, float reloadTime, int maxMagazineSize, float spreadRange)
    {
        _prefab = prefab;
        _name = name;
        _bulletSpeed = bulletSpeed;
        _fireRate = fireRate;
        _reloadTime = reloadTime;
        _maxMagazineSize = maxMagazineSize;
        _remainingBullets = maxMagazineSize;
        _spreadRange = spreadRange;
    }
    
    public void SetRemainingBullets(int remainingBullets)
    {
        _remainingBullets = remainingBullets;
    }
    
    public void SetPrefabPosition(Transform prefab)
    {
        _prefab.transform.SetParent(prefab.transform);
        
        _prefab.transform.position = prefab.transform.position;
        _prefab.transform.rotation = prefab.transform.rotation;
    }
}
