using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private CharacterHealth _WallHealth;
    private GameObject _FakeWalls;

    public int _Num;

    // Start is called before the first frame update
    void Start()
    {
        _WallHealth = GetComponent<CharacterHealth>();
        //_FakeWalls = GameObject.Find("Fake Walls " + _Num);

    }

    // Update is called once per frame
    void Update()
    {
        if (!_WallHealth.IsAlive)
        {
            //_FakeWalls.SetActive(false);
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(false);
            }
        }
    }
}
