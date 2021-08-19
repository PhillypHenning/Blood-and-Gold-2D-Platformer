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
                            - 

            Example;

                                                                            
                |------Bucket2 ------||------Bucket3 ------||------Bucket1 ------|


                                                            P ------->
                                    |------Bucket2 ------||------Bucket3 ------||------Bucket1 ------|   
    

                                                <------- P   
                |------Bucket1 ------||------Bucket2 ------||------Bucket3 ------|
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
