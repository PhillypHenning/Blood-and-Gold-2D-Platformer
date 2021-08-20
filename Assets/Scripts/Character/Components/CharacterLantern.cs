using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLantern : CharacterComponent 
{
    // lantern is usable by player only.

    // Functionality TODO:
    //  - Enable / Disable (hotkey "x")
    //    - Disable should not reduce the light to 0, rather, it should emit very little light
    //  - Reference to Player oil resource (TODO)
    //    - Drains oil while Player is "holding" lantern
    //  - Adjust threshold (hotkey "LEFT ALT")
    //    - This will allow the player to use the scroll wheel to add/reduce oil usage
    //      - more fuel = more light + faster drain rate

    private bool _IsLanternOn;


    protected override void HandleInput()
    {
        base.HandleInput();
    }

}
