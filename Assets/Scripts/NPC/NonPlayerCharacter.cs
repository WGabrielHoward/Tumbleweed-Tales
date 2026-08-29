
using Scripts.UnityBridges;
using Scripts.Components;
using Scripts.Data;
using Scripts.Entities_Sets;
using Scripts.Systems;
using UnityEngine;

namespace Scripts.NPC
{

    [RequireComponent(typeof(EntityBridge))]
    [RequireComponent(typeof(CollisionBridge))]
    public class NonPlayerCharacter : MonoBehaviour
    {
        [Header("Unity Bridges")]
        protected Rigidbody rbThis;
        private EntityBridge entityBridge;

        [Header("Factory Parameters")]
        [SerializeField] private NPCType npcType;

        [SerializeField] protected float speed = 1;
        [SerializeField] protected int health = 10;
        [SerializeField] private float aggroRange;
        [SerializeField] private int damageAmount = 100;
        [SerializeField] private Element element = Element.None;


        [SerializeField] private GameObject target;
        

        void Awake()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();

            entityBridge = gameObject.GetComponent<EntityBridge>();

            EntityFactory.CreateEntity(entityBridge);

            EntityFactory.AddMovement(entityBridge.EntityId, rbThis, speed, Vector3.zero);
            EntityFactory.AddHealth(entityBridge.EntityId, health);
            EntityFactory.AddDamage(entityBridge.EntityId, damageAmount);
            EntityFactory.AddElement(entityBridge.EntityId, element);

            SetTarget(null);

            EntityFactory.AddBehavior(entityBridge.EntityId, target.transform, transform, aggroRange, npcType);


        }

        

        public virtual void SetTarget(GameObject newTarget)
        {
            if (newTarget == null)
            {
                target = GameObject.Find("Player");                
            }
            
            else target = newTarget;

        }
           
               
    }

}
