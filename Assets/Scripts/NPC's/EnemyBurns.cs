
using UnityEngine;


namespace Scripts.NPC
{
    public class EnemyBurns : Enemy
    {

        protected override void Start()
        {
            this.target = GameObject.Find("Player");
            SetEffect(Effect.burn);
        }
    }
}