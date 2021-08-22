using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // TODO: Replace with programattic solution.
    [SerializeField] private string _NameOfPooledObject;
    [SerializeField] private GameObject _ObjectPrefab;
    [SerializeField] private int _PoolSize = 10;
    [SerializeField] private bool _PoolCanExpand = true;

    private Weapon _Weapon;
    private GameObject _ParentPool;
    private List<GameObject> _PooledObjects;

    public string _ObjectPooledFullName { get; set; }


    private void Start()
    {
        _Weapon = GetComponent<Weapon>();
        _ObjectPooledFullName = _Weapon.WeaponName + " " + _NameOfPooledObject + " pool";

        var objExists = GameObject.Find(_ObjectPooledFullName);
        if (objExists)
        {
            _ParentPool = objExists;
        }
        else
        {
            _ParentPool = new GameObject(_ObjectPooledFullName);
        }
        
        Refill();
    }

    public void Refill()
    {   
        if(_PooledObjects == null){
        Debug.Log("Check");
        _PooledObjects = new List<GameObject>();
        for (int i = 0; i < _PoolSize; i++)
        {
            AddGameObjectToPool();
        }
        }
    }

    public GameObject GetGameObjectFromPool()
    {
        for (int i = 0; i < _PooledObjects.Count; i++)
        {
            Debug.Log(_PooledObjects.Count);
            // Check if the pooledObject is already active, if it isn't;
            if (!_PooledObjects[i].activeInHierarchy)
            {
                Debug.Log("Check");
                return _PooledObjects[i];
            }
            if (i == _PooledObjects.Count)
            {
                Debug.Log("Made it to the end");
                if (_PoolCanExpand)
                {
                    AddGameObjectToPool();
                }

            }
        }

        return null;
    }

    public GameObject AddGameObjectToPool()
    {
        GameObject newObject = Instantiate(_ObjectPrefab);
        newObject.SetActive(false);
        newObject.transform.parent = _ParentPool.transform;

        _PooledObjects.Add(newObject);
        return newObject;
    }
}
