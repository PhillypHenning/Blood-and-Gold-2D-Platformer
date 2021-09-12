using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decide Is Character Alive", fileName = "DecideIsCharacterAlive")]
public class DecideCharacterAlive : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return CharacterIsAlive(controller);
    }

    private bool CharacterIsAlive(StateController controller){
        if(controller._Character.IsAlive){
                return true;
            }
        return false;

    }
}
