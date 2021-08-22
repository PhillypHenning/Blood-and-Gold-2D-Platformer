using UnityEngine;
using System.Collections;

public class SingletonExtendedGameObject : MonoBehaviour
{
    public static SingletonExtendedGameObject Instance;

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
}
