using Unity.VisualScripting;
using UnityEngine;


public class EnemyFreezes : Enemy
{
    public override Effect GetEffect()
    {
        return Effect.freeze;
    }
}