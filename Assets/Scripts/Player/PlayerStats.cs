
using UnityEngine;

using Scripts.Systems;
using Scripts.Components;
using NUnit.Framework;
using Scripts.UnityBridges;

namespace Scripts.Player
{
    public class PlayerStats : MonoBehaviour
    {
        private int entityId;
        private int health;
        private LevelCanvas levelCanvas;

        private void Start()
        {
            entityId = gameObject.GetComponent<EntityBridge>().EntityId;
            health = HealthSystem.Instance.GetMaxHealth(entityId);
            levelCanvas = FindAnyObjectByType<LevelCanvas>();
            Assert.IsNotNull(levelCanvas);
            UpdateUI(health);
        }

        void Awake()
        {
            HealthSystem.Instance.OnHealthChanged += HealthChanged;
            DeathSystem.Instance.OnEntityDied += EntityDied;
        }


        public void HealthChanged(int nEntityId, int currentHealth)
        {
            if (nEntityId == entityId)
            {
                UpdateUI(currentHealth);
            }

        }

        public void EntityDied(int deadEntityId)
        {
            if (deadEntityId == entityId)
            {
                GameStateSystem.Instance.TriggerDefeat();
            }
        }

        private void UpdateUI(int health)
        {
            levelCanvas.HealthUpdate(health);
        }

    }
}