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

    public Camera _Camera;
    public Transform _Subject;


    // BACKGROUND \\
    private GameObject _Background1GameObject;
    private SpriteRenderer _Background1SpriteRenderer;
    private Sprite _Background1Sprite; 
    private Rect _Background1Rect;
    private Vector2 _Background1StartPos;
    public Vector2 _BackgroundTravel;

    private float _Background1HorizontalSize;
    private float _Background1VerticalSize;
    private float _Background1ZLayer = 35f;

    // MiddleBackground \\
    private GameObject _MiddleBackground1GameObject;
    private SpriteRenderer _MiddleBackground1SpriteRenderer;
    private Sprite _MiddleBackground1Sprite; 
    private Rect _MiddleBackground1Rect;

    private float _MiddleBackground1HorizontalSize;
    private float _MiddleBackground1VerticalSize;
    private float _MiddleBackground1ZLayer = 21f;

    // MiddleForeground \\
    private GameObject _MiddleForeground1GameObject;
    private SpriteRenderer _MiddleForeground1SpriteRenderer;
    private Sprite _MiddleForeground1Sprite; 
    private Rect _MiddleForeground1Rect;

    private float _MiddleForeground1HorizontalSize;
    private float _MiddleForeground1VerticalSize;
    private float _MiddleForeground1ZLayer = 12;

    // Foreground \\
    private GameObject _Foreground1GameObject;
    private SpriteRenderer _Foreground1SpriteRenderer;
    private Sprite _Foreground1Sprite; 
    private Rect _Foreground1Rect;

    private float _Foreground1HorizontalSize;
    private float _Foreground1VerticalSize;
    private float _Foreground1ZLayer = 5;

    // Start is called before the first frame update
    void Start()
    {
        // BACKGROUND \\
        _Background1GameObject = GameObject.Find("Background_P_1");
        _Background1SpriteRenderer = _Background1GameObject.GetComponent<SpriteRenderer>();
        _Background1Sprite = _Background1SpriteRenderer.sprite;
        
        _Background1Rect = _Background1Sprite.rect;
        _Background1HorizontalSize = _Background1Rect.width; 
        _Background1VerticalSize = _Background1Rect.height;


        // MIDDLEBACKGROUND \\
        _MiddleBackground1GameObject = GameObject.Find("MiddleBackground_P_1");
        _MiddleBackground1SpriteRenderer = _MiddleBackground1GameObject.GetComponent<SpriteRenderer>();
        _MiddleBackground1Sprite = _MiddleBackground1SpriteRenderer.sprite;
        
        _MiddleBackground1Rect = _MiddleBackground1Sprite.rect;
        _MiddleBackground1HorizontalSize = _MiddleBackground1Rect.width; 
        _MiddleBackground1VerticalSize = _MiddleBackground1Rect.height;

        // MiddleForeground \\
        _MiddleForeground1GameObject = GameObject.Find("MiddleForeground_P_1");
        _MiddleForeground1SpriteRenderer = _MiddleForeground1GameObject.GetComponent<SpriteRenderer>();
        _MiddleForeground1Sprite = _MiddleForeground1SpriteRenderer.sprite;
        
        _MiddleForeground1Rect = _MiddleForeground1Sprite.rect;
        _MiddleForeground1HorizontalSize = _MiddleForeground1Rect.width; 
        _MiddleForeground1VerticalSize = _MiddleForeground1Rect.height;

        // Foreground \\
        _Foreground1GameObject = GameObject.Find("Foreground_P_1");
        _Foreground1SpriteRenderer = _Foreground1GameObject.GetComponent<SpriteRenderer>();
        _Foreground1Sprite = _Foreground1SpriteRenderer.sprite;
        
        _Foreground1Rect = _Foreground1Sprite.rect;
        _Foreground1HorizontalSize = _Foreground1Rect.width; 
        _Foreground1VerticalSize = _Foreground1Rect.height;
        
        SetBackgroundLayer();
        SetMiddleBackgroundLayer();
        SetMiddleForegroundLayer();
        SetForegroundLayer();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineBackgroundLoadDirection();
    }

    private void SetBackgroundLayer(float newX=0, float newY=0, float newZ=0){
        _Background1GameObject.transform.position = new Vector3(newX, newY, _Background1ZLayer);
    }

    private void SetMiddleBackgroundLayer(float newX=0, float newY=0, float newZ=0){
        _MiddleBackground1GameObject.transform.position = new Vector3(newX, newY, _MiddleBackground1ZLayer);
    }

    private void SetMiddleForegroundLayer(float newX=0, float newY=0, float newZ=0){
        _MiddleForeground1GameObject.transform.position = new Vector3(newX, newY, _MiddleForeground1ZLayer);
    }

    private void SetForegroundLayer(float newX=0, float newY=0, float newZ=0){
        _Foreground1GameObject.transform.position = new Vector3(newX, newY, _Foreground1ZLayer);
    }


    private void DetermineBackgroundLoadDirection(){
        // Find where the player is

        // Based on the middle point of the current 
    }



    public float MiddleOfBackground(){
        return _Background1HorizontalSize / 2;
    }
}
