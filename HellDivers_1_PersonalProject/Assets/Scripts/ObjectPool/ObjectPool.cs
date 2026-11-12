using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour, IPoolable
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
            var obj = Object.Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(go.transform);
            _pool.Enqueue(obj);
        }
    }

    public T TakePool()
    {
        if (_pool.Count > 0)
        {
            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);

            obj.TakeFromPool();
            
            return obj;
        }

        return null;
    }

    public void ReturnPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
        
        obj.ReturnToPool();
    }
}
