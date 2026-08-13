using System.Collections.Generic;
using UnityEngine;
using Scripts.Components;

using Scripts.Data;
using Scripts.Entities_Sets;

namespace Scripts.Systems
{
    public class DamageSystem : MonoBehaviour
    {

        public static SparseSet<DamageComponent> sparseDamage = new SparseSet<DamageComponent>();

        public static DamageSystem Instance;

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

        public void Register(int entityId, DamageComponent damage)
        {
            sparseDamage.Add(entityId, damage);
        }

        public void Unregister(int entityId)
        {
            sparseDamage.Remove(entityId);
        }


        public int GetDamage(int entityId)
        {
            DamageComponent damage;
            if (!sparseDamage.TryGet(entityId, out damage)) return 0;
            return damage.DamageAmount;
        }

        public void ClearSystem()
        {
            sparseDamage.Clear();
        }
    }
}
