using Scripts.UnityBridges;
using Scripts.Systems;
using UnityEngine;

namespace Scripts.UnityBridges
{

    public class CollisionBridge : MonoBehaviour
    {
        private EntityBridge entityBridge;

        private void Awake()
        {
            entityBridge = GetComponent<EntityBridge>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryResolveCollision(collision.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            TryResolveCollision(other.gameObject);
        }


        private void TryResolveCollision(GameObject other)
        {
            if (!other.TryGetComponent<EntityBridge>(out var otherEntity))
                return;

            Launcher.Instance.CombatSystem.ResolveHit(entityBridge.EntityId, otherEntity.EntityId );
        }
    }
}