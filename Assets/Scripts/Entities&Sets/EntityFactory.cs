
using Scripts.Components;
using Scripts.Data;
using Scripts.Systems;
using Scripts.UnityBridges;
using UnityEngine;

namespace Scripts.Entities_Sets
{
    public static class EntityFactory
    {

        public static int CreateEntity( EntityBridge bridge)
        {
            int entityId = EntityIdGenerator.Next();

            bridge.Initialize(entityId);

            Launcher.Instance.EntityRegistry.Register(entityId, bridge);

            return entityId;
        }


        public static void AddMovement(int entityId, Rigidbody rb, float speed, Vector3 direction )
        {
            MovementComponent movement = new MovementComponent
            {
                rigidbody = rb,
                moveSpeed = speed,
                moveDirection = direction,
                moveInput = 0
            };

            Launcher.Instance.MovementSystem.Register( entityId,  movement);
        }


        public static void AddHealth( int entityId,  int maxHealth)
        {
            HealthComponent health = new HealthComponent
            {
                currentHealth = maxHealth,
                maxHealth = maxHealth
            };

            Launcher.Instance.HealthSystem.Register( entityId,   health);
        }


        public static void AddDamage(  int entityId,   int damage)
        {
            DamageComponent component = new DamageComponent
            {
                DamageAmount = damage
            };

            Launcher.Instance.DamageSystem.Register( entityId,   component);
        }


        public static void AddElement( int entityId,Element element)
        {
            ElementComponent component = new ElementComponent
            {
                elementType = element
            };

            Launcher.Instance.ElementSystem.Register( entityId, component);
        }

        // Refactor BehaviorComponent to take out Unity specifics like Transform.
        // Instead use a Vector3 for positions and have a UnityBridge that updates the position based on the Transform of the GameObject.
        public static void AddBehavior(int entityId, Transform _target, Transform _self, float perception, NPCType npcType)
        {
            BehaviorComponent behaviorComponent = new BehaviorComponent
            {
                target = _target,
                self = _self,
                perceptionRange = perception,
                intent = NPCIntent.Idle,
                type = npcType
            };
            Launcher.Instance.BehaviorSystem.Register(entityId, behaviorComponent);
        }


    }
}