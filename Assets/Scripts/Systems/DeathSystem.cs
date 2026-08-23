using System.Collections.Generic;
using UnityEngine;
using Scripts.Components;
using System;

using Scripts.Entities_Sets;
using Scripts.UnityBridges;

namespace Scripts.Systems
{
    public class DeathSystem 
    {

        public static SparseSet<DeathComponent> sparseDeath = new SparseSet<DeathComponent>();

        private List<int> entitiesToKill = new List<int>();

        public event Action<int> OnEntityDied; // entityId
        private Launcher launcher;

        public DeathSystem()
        {
            launcher = Launcher.Instance;
            launcher.LevelManager.OnLevelChange += ClearSystem; 
        }

        // ---------------------- Registration ----------------------

        public void Register(int entityId, DeathComponent death)
        {
            sparseDeath.Add(entityId, death);
            OnEntityDied?.Invoke(entityId);
        }

        public void Unregister(int entityId)
        {
            sparseDeath.Remove(entityId);
        }


        public float GetDeathDelay(int entityId)
        {
            DeathComponent death;
            if (!sparseDeath.TryGet(entityId, out death)) return 0;
            return death.DeathDelay;
        }

        public void SetDeathDelay(int entityId, float delay)
        {
            DeathComponent death;
            if (!sparseDeath.TryGet(entityId, out death)) return;
            death.DeathDelay = delay;
            sparseDeath.SetComponentByEntity(entityId, death);
        }

        public void FixedUpdate()
        {
            KillExpired();
        }

        private void KillExpired()
        {
            for (int i = 0; i < sparseDeath.Count; i++)
            {
                int entityId = sparseDeath.GetEntityByIndex(i);
                DeathComponent death = sparseDeath.GetComponentByIndex(i);
                death.DeathDelay -= Time.fixedDeltaTime;
                if (death.DeathDelay <= 0)
                {
                    entitiesToKill.Add(entityId);
                }
                else
                {
                    sparseDeath.SetComponentByEntity(entityId, death);
                }
            }
            foreach (int entityId in entitiesToKill)
            {
                Kill(entityId);
            }
            entitiesToKill.Clear();
        }

        private void Kill(int entityId)
        {
            Debug.Log("Killing entity: " + entityId);

            Launcher.Instance.MovementSystem.Unregister(entityId);
            Launcher.Instance.BehaviorSystem.Unregister(entityId);
            Launcher.Instance.DamageSystem.Unregister(entityId);
            Launcher.Instance.ElementSystem.Unregister(entityId);
            Launcher.Instance.HealthSystem.Unregister(entityId);

            Unregister(entityId);

            EntityBridge bridge = Launcher.Instance.EntityRegistry.GetByEntityId(entityId);
            Launcher.Instance.EntityRegistry.Unegister(entityId);
            if(bridge != null)
            {
                bridge.gameObject.SetActive(false);
                //Destroy(bridge.gameObject);
            }
            Debug.Log("Killed entity: " + entityId);
        }

        public bool IsEntityDead(int entityId)
        {
            return sparseDeath.Contains(entityId);
        }

        public void ClearSystem()
        {
            sparseDeath.Clear();
        }
    }
}
