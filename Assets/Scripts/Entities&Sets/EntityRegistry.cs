using Scripts.UnityBridges;
using System.Collections.Generic;
namespace Scripts.Entities_Sets
{

    public class EntityRegistry
    {
        public static EntityRegistry Instance { get; private set; }

        // Entity ↔ GameObject mapping
        private readonly Dictionary<int, EntityBridge> entities = new();

                     

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