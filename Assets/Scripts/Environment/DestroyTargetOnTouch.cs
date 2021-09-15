using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTargetOnTouch: MonoBehaviour
{
    [SerializeField] GameObject _Target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_Target == null) return;
            Destroy(_Target);
            Destroy(gameObject);
        }
    }
}
