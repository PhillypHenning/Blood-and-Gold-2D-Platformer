using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterTypes
    {
        Player, 
        AI
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Class to contain other "Character" GameObjects
    [SerializeField] private CharacterTypes _CharacterType;
    [SerializeField] private GameObject _CharacterSprite;
    [SerializeField] private Animator _CharacterAnimator;

    public CharacterTypes CharacterType => _CharacterType;
    public GameObject CharacterSprite => _CharacterSprite;
    public Animator CharacterAnimator => _CharacterAnimator;   
}
