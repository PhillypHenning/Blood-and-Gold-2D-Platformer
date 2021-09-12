using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{   

    // Get the two parallax managers
    private GameObject _BackgroundSet1;
    private GameObject _BackgroundSet2;

    private ParallaxManager _BackgroundSet1ParallaxManager;
    private ParallaxManager _BackgroundSet2ParallaxManager;

    // Start is called before the first frame update
    void Start()
    {
        _BackgroundSet1 = GameObject.Find("Background Set 1");
        _BackgroundSet2 = GameObject.Find("Background Set 2");

        _BackgroundSet1ParallaxManager = _BackgroundSet1.GetComponent<ParallaxManager>();
        _BackgroundSet2ParallaxManager = _BackgroundSet2.GetComponent<ParallaxManager>();

        _BackgroundSet1.SetActive(false);
        _BackgroundSet2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetParallaxToNewPosition(Transform transform){
        _BackgroundSet1ParallaxManager.ResetParallaxSetLocation(transform);
        _BackgroundSet2ParallaxManager.ResetParallaxSetLocation(transform);
    }

    public void ActivateBuckets(){
        _BackgroundSet1.SetActive(true);
        _BackgroundSet2.SetActive(true);
    }
}
