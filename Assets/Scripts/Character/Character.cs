using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Global character state variables
    private bool _IsGrounded;
    private bool _IsLocked;

    // public property accessors
    public bool IsLocked { get => _IsLocked; set => _IsLocked = value; }
    public bool IsGrounded { get => _IsGrounded; set => _IsGrounded = value; }

    public enum CharacterTypes
    {
        Player, 
        AI
    }

    // Class to contain other "Character" GameObjects
    [SerializeField] private CharacterTypes _CharacterType;
    [SerializeField] private GameObject _CharacterSprite;

    public CharacterTypes CharacterType => _CharacterType;
    public GameObject CharacterSprite => _CharacterSprite;
}
