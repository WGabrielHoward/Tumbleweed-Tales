
using UnityEngine;

namespace Scripts.NPC
{
    public class EnemyFreezes : Enemy
    {


        protected override void Start()
        {
            this.target = GameObject.Find("Player");
            SetEffect(Effect.freeze);
        }
    }
}