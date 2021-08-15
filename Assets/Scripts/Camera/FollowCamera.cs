using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private float _cameraDistance = -12f;

    private void Update()
    {
        // TODO: add camera smoothing and verify performance
        transform.position = new Vector3(_player.position.x, _player.position.y + 2f, _cameraDistance);
    }
}
