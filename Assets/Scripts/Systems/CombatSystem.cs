using UnityEngine;
using Scripts.Components;
using Scripts.UnityBridges;

namespace Scripts.Systems
{

    public class CombatSystem : MonoBehaviour
    {
        public static CombatSystem Instance { get; private set; }
        

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

        }


        public void ResolveHit(int attacker, int defender)
        {
            Debug.Log("ResolveHit: Attacker ID: " + attacker + ", Defender ID: " + defender);

            int damage = DamageSystem.Instance.GetDamage(attacker);
            { // was an if damage>0 statement but that negated healing

                Element attackElement = ElementSystem.Instance.GetElement(attacker);

                Element defendElement = ElementSystem.Instance.GetElement(defender);

                int finalDamage = ElementRules.CalculateDamage(damage, attackElement, defendElement);

                HealthSystem.Instance.ApplyDamage(defender, finalDamage);

                //ApplyStatusEffects(...);
            }
           
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision Enter with " + other.gameObject.name);
            if (other.gameObject.GetComponent<EntityBridge>())
            {
                int attackerId = other.gameObject.GetComponent<EntityBridge>().EntityId;
                int defenderId = this.gameObject.GetComponent<EntityBridge>().EntityId;
                ResolveHit(attackerId, defenderId);
            }

        }

        private void OnCollisionExit(Collision other)
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<EntityBridge>())
            {
                int attackerId = other.gameObject.GetComponent<EntityBridge>().EntityId;
                int defenderId = this.gameObject.GetComponent<EntityBridge>().EntityId;
                ResolveHit(attackerId, defenderId);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            
        }

        
    }
}