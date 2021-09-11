using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyWeaponsHeld(){
        foreach(Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        Destroy(this.gameObject);
    }
}
