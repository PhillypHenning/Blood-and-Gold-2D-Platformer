using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    /*
        Notes for later

        Parallax Script     - Attach to the Background Set GameObject buckets, responsible for moving background piece by Unity setting ParallaxFactor
        Parallax Manager    - Discover the Background Buckets, get reference to spriterenderers.
                            - Determine what bucket the Camera is on
                            - When the player transitions between sets, load the furtherest background bucket towards the players upcoming position
                            - Player should start in a midway position between two buckets
                            
                            - Something to consider;
                                - The backgrounds move at different speeds so they will need to click together at different rates

            Example;

                                                                           
                |------Bucket2 ------||------Bucket3 ------||------Bucket1 ------|


                                                            P ------->
                                    |------Bucket2 ------||------Bucket3 ------||------Bucket1 ------|   
    

                                                <------- P   
                |------Bucket1 ------||------Bucket2 ------||------Bucket3 ------|
    */


    // Let's get one layer working then expand
    /*
        Needs to know 
    */
    [SerializeField] private SpriteRenderer _BackgroundSpriteRenderer;
    private Sprite _BackgroundSprite; 
    

    [SerializeField] private float _BackgroundDepthZ = 35f; // 35 default


    // Start is called before the first frame update
    void Start()
    {
        _BackgroundSprite = _BackgroundSpriteRenderer.GetComponent<Sprite>();
        Debug.Log(_BackgroundSprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetBackground(){

    }
}
