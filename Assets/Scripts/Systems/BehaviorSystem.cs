using Scripts.Components;
using Scripts.Data;
using Scripts.Entities_Sets;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Systems
{

    public class BehaviorSystem : MonoBehaviour
    {

        private SparseSet<BehaviorComponent> sparseBehavior = new SparseSet<BehaviorComponent>();



        private float behaviorTickRate = 0.2f; // 5 Hz
        private float behaviorTimer;

        public void Start()
        {
            Launcher.Instance.LevelManager.OnLevelChange += ClearSystem;            

        }

        public void Register(int entityId, BehaviorComponent behavior)
        {
            sparseBehavior.Add(entityId, behavior);
        }


        public void Unregister(int entityId)
        {
            sparseBehavior.Remove(entityId);
        }


        void Update()
        {
            behaviorTimer -= Time.deltaTime;
            if (behaviorTimer > 0f)
                return;

            behaviorTimer = behaviorTickRate;
            RunBehavior();


        }

        private void RunBehavior()
        {
            var healthSystem = Launcher.Instance.HealthSystem;
            var movementSystem = Launcher.Instance.MovementSystem;

            for (int i = 0; i < sparseBehavior.Count; i++)
            {
                BehaviorComponent behavior = sparseBehavior.GetComponentByIndex(i);
                int entityId = sparseBehavior.GetEntityByIndex(i);
                
                Vector3 toTarget = behavior.target.position - behavior.self.position;

                // Decide intent
                if (toTarget.sqrMagnitude < behavior.perceptionRange * behavior.perceptionRange)
                {
                    behavior.intent = NPCIntent.Flee;
                    // asking npc for health... is this the right call? (because right now healthSystem is trying to update npcHealth)
                    if (behavior.type == NPCType.Enemy && healthSystem.GetCurrentHealth(entityId) > 2)
                    {
                        behavior.intent = NPCIntent.Chase;
                    }

                }
                else
                {
                    behavior.intent = NPCIntent.Idle;
                }
                sparseBehavior.SetComponentByEntity(entityId, behavior);

                // Compute desired direction
                Vector3 direction = Vector3.zero;
                int input = 0;

                switch (behavior.intent)
                {
                    case NPCIntent.Flee:
                        direction = -toTarget.normalized;
                        input = 1;
                        break;

                    case NPCIntent.Chase:
                        direction = toTarget.normalized;
                        input = 1;
                        break;
                }

                movementSystem.SetMoveIntentByEntity(entityId, direction, input);

            }


        }

        public BehaviorComponent GetBehaviorByEntity(int entityId)
        {
            if (sparseBehavior.TryGet(entityId, out BehaviorComponent behavior))
            {
                return behavior;
            }
            return default;
        }

        public void SetBehaviorByEntity(int entityId, BehaviorComponent newBehavior)
        {
            if (sparseBehavior.TryGet(entityId, out BehaviorComponent behavior))
            {
                sparseBehavior.SetComponentByEntity(entityId, newBehavior);
            }
        }

        public void SetIntentByEntity(int entityId, NPCIntent newIntent)
        {
            if (sparseBehavior.TryGet(entityId, out BehaviorComponent behavior))
            {
                behavior.intent = newIntent;
                sparseBehavior.SetComponentByEntity(entityId, behavior);
            }

        }

        public void SetTargetByEntity(int entityId, Transform newTarget)
        {
            if (sparseBehavior.TryGet(entityId, out BehaviorComponent behavior))
            {
                behavior.target = newTarget;
                sparseBehavior.SetComponentByEntity(entityId, behavior);
            }
        }

        public void SetPerceptionRangeByEntity(int entityId, float newPerceptionRange)
        {
            if (sparseBehavior.TryGet(entityId, out BehaviorComponent behavior))
            {
                behavior.perceptionRange = newPerceptionRange;
                sparseBehavior.SetComponentByEntity(entityId, behavior);
            }

        }

        private void ClearSystem()
        {
            Debug.Log($"ClearBehaviorSystem - Before: {sparseBehavior.Count}");

            sparseBehavior.Clear();

            Debug.Log($"ClearBehaviorSystem - After: {sparseBehavior.Count}");
        }

    }

}
