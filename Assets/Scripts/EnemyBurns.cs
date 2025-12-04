using Unity.VisualScripting;
using UnityEngine;


public class EnemyBurns : Enemy
{
    public override Effect GetEffect()
    {
        return Effect.burn;
    }
}