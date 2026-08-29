using UnityEngine;

namespace Scripts.UnityBridges
{
    public class EntityBridge: MonoBehaviour
    {
        public int EntityId { get; private set; }
        
        public void Initialize(int entityId)
        {
            this.EntityId = entityId;
        }



    }
}
