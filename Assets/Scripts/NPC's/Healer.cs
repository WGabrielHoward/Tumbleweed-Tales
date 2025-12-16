
using UnityEngine;


namespace Scripts.NPC
{
    public class Healer : NonPlayerCharacter
    {

        protected override void Start()
        {
            SetEffect(Effect.heal);
        }

    }
}