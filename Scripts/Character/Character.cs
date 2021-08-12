using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // global variables example
    public bool _IsGrounded;

    public enum CharacterTypes
    {
        Player, 
        AI
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    // Class to contain other "Character" GameObjects
    [SerializeField] private CharacterTypes _CharacterType;
    [SerializeField] private GameObject _CharacterSprite;

    public CharacterTypes CharacterType => _CharacterType;
    public GameObject CharacterSprite => _CharacterSprite;
}
