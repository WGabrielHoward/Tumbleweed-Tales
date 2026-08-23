
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
            health = Launcher.Instance.HealthSystem.GetMaxHealth(entityId);
            levelCanvas = FindAnyObjectByType<LevelCanvas>();
            Assert.IsNotNull(levelCanvas);
            UpdateUI(health);
        }

        void Awake()
        {
            Launcher.Instance.HealthSystem.OnHealthChanged += HealthChanged;
            Launcher.Instance.DeathSystem.OnEntityDied += EntityDied;
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
                Launcher.Instance.GameStateSystem.TriggerDefeat();
            }
        }

        private void UpdateUI(int health)
        {
            levelCanvas.HealthUpdate(health);
        }

    }
}