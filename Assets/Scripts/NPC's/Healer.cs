
using UnityEngine;


namespace Scripts.NPC
{
    public class Healer : NonPlayerCharacter
    {

        protected override void Start()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
            //this.target = GameObject.Find("Player");
            thisEffect = gameObject.AddComponent<EffectScript>();
            SetEffect(Effect.heal);
        }

    }
}