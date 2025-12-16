
using UnityEngine;

namespace Scripts.NPC
{
    public class EnemyPoisons : Enemy
    {


        protected override void Start()
        {
            this.target = GameObject.Find("Player");
            SetEffect(Effect.poison);
        }
    }
}