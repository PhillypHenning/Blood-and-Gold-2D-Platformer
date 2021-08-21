using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Summary
    // 1. - Determine environment size
    // 2. - If player moves on the x axis, follow player using full parallax effect
    // 3.   - else; if the player is moving on the y axis, follow player instead. 
    // 4. - 


    private Vector2 _StartPosition;
    private SpriteRenderer _SpriteRenderer;
    private float _ZPosStart;

    private GameObject _Subject;
    private Camera _Camera;
    private Transform _SubjectTransform;

    private Vector2 Travel => (Vector2)_Camera.transform.position - _StartPosition;
    private float _DistanceFromSubject => transform.position.z - _SubjectTransform.position.z; 
    private float _ClippingPlane => (_Camera.transform.position.z + (_DistanceFromSubject > 0 ? _Camera.farClipPlane : _Camera.nearClipPlane));

    private float _ParallaxFactor => Mathf.Abs(_DistanceFromSubject / _ClippingPlane);

    private Sprite _Sprite;
    private Rect _Rect;
    private float _MiddlePos;

    private void Awake() {
        _Subject = GameObject.Find("Player");
        _Camera = _Subject.GetComponentInChildren<Camera>();
        _SubjectTransform = _Subject.transform;
    }

    private void Start() {     
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Sprite = _SpriteRenderer.sprite;
        _Rect = _Sprite.rect;
        _MiddlePos = _Rect.width / 2;

        Debug.Log(_Sprite.bounds.size.x);
        Debug.Log(_Sprite.bounds.size.y);

        _StartPosition = transform.position;
        _ZPosStart = transform.position.z;
    }

    private void Update()
    {
        //Vector2 newPos = _StartPosition + Travel * _ParallaxFactor;
        //transform.position = new Vector3(newPos.x, newPos.y, _ZPosStart);
    }

    private void FixedUpdate()
    {
        StartParallaxEffect();
        DetermineBackgroundLoadDirection();
    }

    private void StartParallaxEffect(){
        Vector2 newPosX = _StartPosition + Travel * _ParallaxFactor;
        Vector2 newPosY = ((Vector2)_Subject.transform.position + Travel * _ParallaxFactor);
        
        transform.position = new Vector3(newPosX.x, newPosY.y, _ZPosStart);
    }

    private void DetermineBackgroundLoadDirection(){
        // Determine where the subject is relative to the position between either side and the half way point
        //float cur = 
        //Debug.Log(_Rect);
        Vector3 distsub1 = new Vector3(_StartPosition.x, 0, 0);
        Vector3 distsub2 = new Vector3(_Subject.transform.position.x, 0, 0);
        
        float Xdistance = Vector3.Distance(distsub1, distsub2);
        //Debug.Log(_ParallaxFactor);
        //Debug.Log("Redrawn when " +  (1000 * _ParallaxFactor / 3));
        //Debug.Log(Xdistance);
        float redrawAt = (1000 * _ParallaxFactor / 2.75f);
        if(Xdistance > redrawAt){
            Debug.Log("Redrawing layer: " + this.name);
            // TODO: Enable the second set
        }

    }


}
