using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private GameObject _BackgroundSet1;
    private GameObject _BackgroundSet2;
    private GameObject _BackgroundSet3;
    private SpriteRenderer _BackgroundSpriteRenderer1;
    private SpriteRenderer _BackgroundSpriteRenderer2;
    private SpriteRenderer _BackgroundSpriteRenderer3;

    //_XLength = _SpriteRenderer.bounds.size.x;
    //_YHeight = _SpriteRenderer.bounds.size.y;


    // Start is called before the first frame update
    void Start()
    {
        //_SpriteRenderer = GetComponent<SpriteRenderer>();
        //_BackgroundSet1 = GameObject.Find("Background Set 1");
        //_BackgroundSet2 = GameObject.Find("Background Set 2");
        //_BackgroundSet3 = GameObject.Find("Background Set 3");
        //_BackgroundSpriteRenderer1 = _BackgroundSet1.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ReportWhichBucket(){
        //if(_Camera.transform.position.x < _BackgroundSet1)
    }

    private void Loop(){
        // float temp = (_Camera.transform.position.x * (1 - _ParallaxFactor));

        // if(temp > _StartPosition.x + _XLength){
        //     _StartPosition.x +=_XLength;
        // }else if(temp < _StartPosition.x - _XLength){
        //     _StartPosition.x -=_XLength;
        // }
    }
}
