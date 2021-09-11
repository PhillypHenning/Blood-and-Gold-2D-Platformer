using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentSnuffLight : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Sconce"){
            other.GetComponent<Interactable_Sconce>().TurnOffLight();
        }   
    }
}
