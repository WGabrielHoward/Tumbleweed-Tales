using Unity.VisualScripting;
using UnityEngine;


public class Healer : NonPlayerCharacter
{

    public override Effect GetEffect()
    {
        return Effect.heal;
    }
}
