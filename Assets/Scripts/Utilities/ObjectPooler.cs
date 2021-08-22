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
        _ObjectPooledFullName = _Weapon._WeaponOwner.CharacterType + " " + _Weapon.WeaponName + " " + _NameOfPooledObject + " pool";
        
        _ParentPool = new GameObject(_ObjectPooledFullName);
        
        Refill();
    }

    public void Refill()
    {
        if (_PooledObjects == null)
        {
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
            // Check if the pooledObject is already active, if it isn't;
            if (!_PooledObjects[i].activeInHierarchy)
            {
                return _PooledObjects[i];
            }
        
            if (i == _PooledObjects.Count - 2)
            {
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
