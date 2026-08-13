using Scripts.UnityBridges;
using Scripts.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace Scripts.Entities_Sets
{

    public class EntityRegistry : MonoBehaviour
    {
        public static EntityRegistry Instance { get; private set; }

        // Entity ↔ GameObject mapping
        private readonly Dictionary<int, EntityBridge> entities = new();

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
               

        public void Register(int entityId, EntityBridge entityBridge)
        {
            entities.Add(entityId, entityBridge);

        }

        public void Unegister(int entityId)
        {
            entities.Remove(entityId);

        }

        public EntityBridge GetByEntityId(int entityId)
        {
            EntityBridge entityBridge;
            entities.TryGetValue(entityId, out entityBridge);
            return entityBridge;
        }

        public void ClearRegistry()
        {
            entities.Clear();
        }
    }
}