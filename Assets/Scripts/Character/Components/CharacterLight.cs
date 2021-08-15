using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLight : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void Update()
    {
        // TODO: add camera smoothing and verify performance
        transform.position = new Vector3(_player.position.x, _player.position.y + 2f, 0);
    }
}
