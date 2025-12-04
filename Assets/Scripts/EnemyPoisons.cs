using Unity.VisualScripting;
using UnityEngine;


public class EnemyPoisons : Enemy
{
    public override Effect GetEffect()
    {
        return Effect.poison;
    }
}