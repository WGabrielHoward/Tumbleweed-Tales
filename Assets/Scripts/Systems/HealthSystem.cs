using Scripts.Components;
using Scripts.Entities_Sets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Systems
{
    public class HealthSystem : MonoBehaviour
    {
        public static HealthSystem Instance { get; private set; }

        private SparseSet<HealthComponent> sparseHealth = new SparseSet<HealthComponent>();

        // Pure signals — no gameplay logic
        public event Action<int, int> OnHealthChanged; // entityId, newHealth

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.OnLevelChange += ClearSystem;
            }
        }

        // ---------------------- Registration ----------------------

        public void Register(int entityId, HealthComponent health)
        {
            sparseHealth.Add(entityId, health);
        }

        public void Unregister(int entityId)
        {
            sparseHealth.Remove(entityId);
        }

        // ---------------------- Damage ----------------------

        public void ApplyDamage(int entityId, int amount)
        {
            Debug.Log($"{amount} Damage applied to entity {entityId}");
            HealthComponent health;
            if (!sparseHealth.TryGet(entityId, out health)) return;
            
            health.currentHealth -= amount;

            // in case of healing via negative damage
            if (health.currentHealth > health.maxHealth)
                health.currentHealth = health.maxHealth;

            sparseHealth.SetComponentByEntity(entityId, health);
            OnHealthChanged?.Invoke(entityId, health.currentHealth);


            if (health.currentHealth <= 0 && !DeathSystem.Instance.IsEntityDead(entityId))
            {
                Debug.Log("Entity " + entityId + " has died.");
                AttachDeath(entityId);
                return;
            }


        }

        public void Heal(int entityId, int amount)
        {
            HealthComponent health;
            if (!sparseHealth.TryGet(entityId, out health)) return;

            

            health.currentHealth += amount;

            if (health.currentHealth > health.maxHealth)
                health.currentHealth = health.maxHealth;

            sparseHealth.SetComponentByEntity(entityId, health);

        }

        // ---------------------- Death Handling ----------------------
        
        private void AttachDeath(int entityId)
        {
            DeathComponent death = new DeathComponent() 
            {
                DeathDelay = 2f     // I need to set the delay by entity type or additional logic
            };
            DeathSystem.Instance.Register(entityId, death);
        }

        // ---------------------- Queries ----------------------

        public int GetCurrentHealth(int entityId)
        {
            HealthComponent health;
            if (!sparseHealth.TryGet(entityId, out health)) return 0;
            return health.currentHealth;
        }

        public int GetMaxHealth(int entityId)
        {
            HealthComponent health;
            if (!sparseHealth.TryGet(entityId, out health)) return 0;
            return health.maxHealth;
        }
        
        // --------------------- Clear ------------------------------
        
        public void ClearSystem()
        {
            sparseHealth.Clear();
        }

    }
}

