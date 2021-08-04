using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodge : CharacterComponent
{
    private float _DodgeSpeed = 1000f;
    private float _DodgeDuration = .3f;
    private float _DodgeTimer;
    private bool _CanDodge;

    private Vector2 _DodgeOrigin;
    private Vector2 _DodgeDestion;
    private Vector2 _DodgeNewPosition;

    protected override void HandleInput()
    {
        if(_Character.CharacterType == Character.CharacterTypes.Player && Input.GetKeyDown(KeyCode.LeftShift)){
            // Dodge here
            Dodge();
        }
    }


}
