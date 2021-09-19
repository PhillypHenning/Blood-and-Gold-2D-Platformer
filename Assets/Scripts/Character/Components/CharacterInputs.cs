using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputs : MonoBehaviour
{
    // Jumping
    private KeyCode _JumpKeyCode = KeyCode.Space; 
    public KeyCode JumpKeyCode {get => _JumpKeyCode; set => _JumpKeyCode = value;}

    // Dodging
    private KeyCode _DodgeKeyCode = KeyCode.LeftShift;
    public KeyCode DodgeKeyCode {get => _DodgeKeyCode; set => _DodgeKeyCode = value;}
}
