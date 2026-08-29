using UnityEngine;
using Scripts.Systems;
using Scripts.Entities_Sets;
using Scripts.Components;
using System;
using Scripts.UnityBridges;

namespace Scripts.Player
{
    [RequireComponent(typeof(EntityBridge))]
    [RequireComponent(typeof(CollisionBridge))]
    public class PlayerEntity : MonoBehaviour
    {
        [Header("Unity Bridges")]
        private Rigidbody rb;
        private PlayerInput playerInput;
        private EntityBridge entityBridge;

        [Header("Factory Parameters")]
        [SerializeField] private float playerSpeed=5f;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int damageAmount = 1;
        [SerializeField] private Element element = Element.None;



        private void Awake()
        {           

            rb = gameObject.GetComponent<Rigidbody>();
            playerInput = gameObject.GetComponent<PlayerInput>();
            entityBridge = gameObject.GetComponent<EntityBridge>();

            EntityFactory.CreateEntity(entityBridge);
            playerInput.SetPlayerID(entityBridge.EntityId);

            EntityFactory.AddMovement(entityBridge.EntityId, rb, playerSpeed, Vector3.zero);
            EntityFactory.AddHealth(entityBridge.EntityId, maxHealth);
            EntityFactory.AddDamage(entityBridge.EntityId, damageAmount);
            EntityFactory.AddElement(entityBridge.EntityId, element);
                        
            
        }

       
    }
}
