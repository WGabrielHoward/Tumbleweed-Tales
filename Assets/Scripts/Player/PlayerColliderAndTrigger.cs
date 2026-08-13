
using Scripts.Systems;
using Scripts.UnityBridges;
using UnityEngine;
using Scripts.Components;

namespace Scripts.Player
{
    // Definitely needs refactored

    [RequireComponent(typeof(PlayerEffects))]
    public class PlayerColliderAndTrigger : MonoBehaviour
    {
        private PlayerEffects playerEffects;

        private void Awake()
        {
            playerEffects = GetComponent<PlayerEffects>();
        }

        private void OnCollisionEnter(Collision other)
        {
            HandleEnter(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleEnter(other.gameObject);
        }

        private void OnCollisionExit(Collision other)
        {
            HandleExit(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            HandleExit(other.gameObject);
        }

        private void HandleEnter(GameObject obj)
        {
            if (obj.TryGetComponent<EntityBridge>(out var bridge))
            {
                Element element = ElementSystem.Instance.GetElement(bridge.EntityId);
                var effect = ElementRules.GetStatusForElement(element);
                playerEffects.EffectsSwitch(effect, true);
            }

            if (obj.CompareTag("Victory"))
            {
                GameStateSystem.Instance.TriggerVictory();
            }
        }

        private void HandleExit(GameObject obj)
        {
            if (obj.TryGetComponent<EntityBridge>(out var bridge))
            {
                Element element = ElementSystem.Instance.GetElement(bridge.EntityId);
                var effect = ElementRules.GetStatusForElement(element);
                playerEffects.EffectsSwitch(effect, false);
            }
        }
    }
}
