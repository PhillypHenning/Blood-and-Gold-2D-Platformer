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




    private void Start()
    {
        _Weapon = GetComponent<Weapon>();

        _ParentPool = new GameObject(_Weapon.WeaponName + " " + _NameOfPooledObject + " pool");
        Refill();
    }

    public void Refill(){
        _PooledObjects = new List<GameObject>();
        for (int i = 0; i < _PoolSize; i++)
        {
            AddGameObjectToPool();
        }
    }

    public GameObject GetGameObjectFromPool(){
        for (int i = 0; i < _PooledObjects.Count; i++)
        {
            // Check if the pooledObject is already active, if it isn't;
            if(!_PooledObjects[i].activeInHierarchy){
                Debug.Log("Check");
                return _PooledObjects[i];
            }

            if(_PoolCanExpand){
                AddGameObjectToPool();
            }
        }
        
        return null;
    }

    public GameObject AddGameObjectToPool(){
        GameObject newObject = Instantiate(_ObjectPrefab);
        newObject.SetActive(false);
        newObject.transform.parent = _ParentPool.transform;

        _PooledObjects.Add(newObject);
        return newObject;
    }
}
