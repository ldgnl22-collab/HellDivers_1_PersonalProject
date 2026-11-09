using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    private Queue<T> _pool = new Queue<T>();
    private T _prefab;
    private Transform _parent;

    public ObjectPool(T prefab, int initSize, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
        
        GameObject go = new GameObject(typeof(T).Name);
        go.transform.parent = _parent;

        for (int i = 0; i < initSize; i++)
        {
        }
    }
}
