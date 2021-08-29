using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentFlip : MonoBehaviour
{
    private Character _Character;
    private BoxCollider2D _BoxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _Character= GetComponentInParent<Character>();
        _BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    private void Flip(){
        if (_Character.FacingRight)
        {
            _BoxCollider2D.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            _BoxCollider2D.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
