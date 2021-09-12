using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Summary
    // 1. - Determine environment size
    // 2. - If player moves on the x axis, follow player using full parallax effect
    // 3.   - else; if the player is moving on the y axis, follow player instead. 
    // 4. - Determine when the next environment needs to draw


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

    private Vector3 _MiddlePos;
    private float _Radius;

    private GameObject _MapBucket;
    private GameObject _AltMapBucket;
    private Transform _AltMapLayer;
    private Parallax _AltParallaxScript;
    private int _AltNum;
    private string _AltBucketName;
    private bool _NextZoneLoaded;
    private bool _LoadLeftZoneOff = false;

    private void Awake()
    {
        _Subject = GameObject.FindWithTag("Player");
        _Camera = _Subject.GetComponentInChildren<Camera>();
        _SubjectTransform = _Subject.transform;
    }

    private void Start()
    {

        _SpriteRenderer = GetComponent<SpriteRenderer>();

        // Get current Map bucket
        _MapBucket = this.transform.parent.gameObject;
        if (_MapBucket.name == "Background Set 1")
        {
            _AltBucketName = "Background Set 2";
            _AltNum = 2;
        }
        else if (_MapBucket.name == "Background Set 2")
        {
            this.gameObject.SetActive(false);
            _AltBucketName = "Background Set 1";
            _AltNum = 1;
        }
        else if (_MapBucket.name == "Background Set 3")
        {
            Debug.Log("check23");
            _AltBucketName = "Background Set 1";
            _AltNum = 1;
        }
        _AltMapBucket = GameObject.Find(_AltBucketName);
        var goname = this.name.Remove(this.name.Length - 1);
        
        _AltMapLayer = _AltMapBucket.transform.Find(goname + _AltNum);
        _AltParallaxScript = _AltMapLayer.GetComponent<Parallax>();

        SetDefaults();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!_LoadLeftZoneOff)
        {
            StartParallaxEffect();
            DetermineBackgroundLoadDirection();
        }
    }

    private void StartParallaxEffect()
    {
        Vector2 newPosX = _StartPosition + Travel * _ParallaxFactor;

        // Increase y axis range, but makes the camera far more jumpy
        Vector2 newPosY = ((Vector2)_Subject.transform.position + Travel * _ParallaxFactor);

        transform.position = new Vector3(newPosX.x, newPosX.y, _ZPosStart);
    }

    private void DetermineBackgroundLoadDirection()
    {
        // Determine where the subject is relative to the position between either side and the half way point
        //float cur = 
        //Debug.Log(_Rect);

        // What is the total length of the environment?
        // Cut that in half = Middle
        // Cut that in half = Load position

        Vector3 LeftLoadZone = new Vector3(_MiddlePos.x - _Radius / 2, 0, 0);
        Vector3 RightLoadZone = new Vector3(_MiddlePos.x + _Radius / 2, 0, 0);

        Vector3 LeftMapEdge = new Vector3(_MiddlePos.x - _Radius, 0, 0);
        Vector3 RightMapEdge = new Vector3(_MiddlePos.x + _Radius, 0, 0);

        // Load Zones
        if (_Subject.transform.position.x < LeftLoadZone.x)
        {
            //Debug.Log("Player entered left loading zone for " + this.name);
            // Load next map set on left hand size of screen
            LoadNextZone(LeftMapEdge, true);
        }
        else if (_Subject.transform.position.x > RightLoadZone.x)
        {
            LoadNextZone(RightMapEdge);
        }
        else
        {
            // Debug.Log("No loading zone reached");
            // Unload next map until needed
        }
    }

    private void LoadNextZone(Vector3 newMapPos, bool loadLeft = false)
    {
        // newMapPos = the very edge of the current map
        // Summary
        //  1. Determine which way the next map needs to load in from
        if (_LoadLeftZoneOff) { return; }
        float TotalLength = (_Radius * 2) - 15;
        if (loadLeft && !_NextZoneLoaded)
        {
            // Snap the right side of the next map to the left side of the current map
            //Debug.Log("Loading next zone for: " + this.name);
            // This part of the script will need to know about;
            //  1. The other disabled map bucket
            //  2. The layers inside that bucket


            // Loading;
            //  1. newMapPos = The edge of the current map
            //  2. The next map should be loaded with the oppsite edge on newMapPos
            // That means the new position = Current position +- half of the total size (raidus) 

            // Move the altbucket gameobject
            Vector3 newPos = new Vector3(_StartPosition.x - TotalLength, _StartPosition.y, _ZPosStart);

            _AltParallaxScript.ResetMapStartPosition(newPos);
            _AltParallaxScript.SetActive(true);
            _NextZoneLoaded = true;
        }
        else if (!loadLeft && !_NextZoneLoaded)
        {
            // Snap the left side of the next map to the right side of the current map
            Vector3 newPos = new Vector3(_StartPosition.x + TotalLength, _StartPosition.y, _ZPosStart);

            _AltParallaxScript.ResetMapStartPosition(newPos);
            _AltParallaxScript.SetActive(true);
            _NextZoneLoaded = true;
        }

    }

    private void SetDefaults()
    {
        _StartPosition = transform.position;
        _ZPosStart = transform.position.z;

        _MiddlePos = _SpriteRenderer.bounds.center;
        _Radius = _SpriteRenderer.bounds.extents.magnitude;
        _NextZoneLoaded = false;
    }

    public void ResetMapStartPosition(Vector3 newPos)
    {
        this.transform.position = newPos;
        SetDefaults();
    }

    public void SetActive(bool flag = false)
    {
        this.gameObject.SetActive(flag);
    }

    public void ResetMapPosition(Transform transform)
    {
        _LoadLeftZoneOff = true;
        this.transform.position = transform.position;
        SetDefaults();
        _LoadLeftZoneOff = false;
    }
}
