using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Vector2 _StartPosition;
    private float _ZPosStart;

    public Camera _Camera;
    public Transform _Subject;

    public Vector2 Travel => (Vector2)_Camera.transform.position - _StartPosition;
    public float _DistanceFromSubject => transform.position.z - _Subject.position.z; 
    public float _ClippingPlane => (_Camera.transform.position.z + (_DistanceFromSubject > 0 ? _Camera.farClipPlane : _Camera.nearClipPlane));

    public float _ParallaxFactor => Mathf.Abs(_DistanceFromSubject / _ClippingPlane);

    private void Start() {
        _StartPosition = transform.position;
        _ZPosStart = transform.position.z;  
    }

    public void Update()
    {
        Debug.Log("_StartPosition " + _StartPosition);
        Debug.Log("Travel " + Travel);
        Debug.Log("_ParallaxFactor " + _ParallaxFactor);
        Vector2 newPos = _StartPosition + Travel * _ParallaxFactor;
        Debug.Log("newPos " + newPos);

        transform.position = new Vector3(newPos.x, newPos.y, _ZPosStart);
    }


}
