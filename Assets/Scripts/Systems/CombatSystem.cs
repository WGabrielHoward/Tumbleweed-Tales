using UnityEngine;
using Scripts.Components;
using Scripts.UnityBridges;

namespace Scripts.Systems
{

    public class CombatSystem 
    {
        
        public CombatSystem()
        {

        }


        public void ResolveHit(int attacker, int defender)
        {
            Debug.Log("ResolveHit: Attacker ID: " + attacker + ", Defender ID: " + defender);

            int damage = Launcher.Instance.DamageSystem.GetDamage(attacker);
            { // was an if damage>0 statement but that negated healing

                Element attackElement = Launcher.Instance.ElementSystem.GetElement(attacker);

                Element defendElement = Launcher.Instance.ElementSystem.GetElement(defender);

                int finalDamage = ElementRules.CalculateDamage(damage, attackElement, defendElement);

                Launcher.Instance.HealthSystem.ApplyDamage(defender, finalDamage);

                //ApplyStatusEffects(...);
            }
           
        }

       

        
    }
}