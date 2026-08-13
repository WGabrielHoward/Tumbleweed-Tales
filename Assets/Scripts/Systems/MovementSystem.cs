using Scripts.Components;
using Scripts.Entities_Sets;
using UnityEngine;

namespace Scripts.Systems
{
    
    public class MovementSystem : MonoBehaviour
    {
        
        public static MovementSystem Instance { get; private set; }

        private SparseSet<MovementComponent> sparseMovement = new SparseSet<MovementComponent>();


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
                Debug.Log("Subscribe to ClearMovementSystem");
                LevelManager.Instance.OnLevelChange += ClearSystem;
            }
        }

        private void FixedUpdate()
        {

            for (int i = 0; i < sparseMovement.Count; i++)
            {
                MovementComponent tmpComp = sparseMovement.GetComponentByIndex(i);
                if (tmpComp.rigidbody != null)
                {
                    tmpComp.rigidbody.AddForce(tmpComp.moveDirection * tmpComp.moveInput * tmpComp.moveSpeed);
                }             
            }

        }

        public void Register(int entityId, MovementComponent component)
        {
            sparseMovement.Add(entityId, component);

        }

        public void Unregister(int entityId)
        {
            sparseMovement.Remove(entityId);
        }

        public void SetMovementByEntity(int entityId, Vector3 newDirection, int newInput, float newSpeed)
        {
            if (sparseMovement.TryGet(entityId, out MovementComponent component))
            {
                component.moveSpeed = newSpeed;
                component.moveDirection = newDirection;
                component.moveInput = newInput;
                sparseMovement.SetComponentByEntity(entityId, component);
            }
        }

        public void SetMoveDirectionByEntity(int entityId, Vector3 newDirection)
        {
            if (sparseMovement.TryGet(entityId, out MovementComponent component))
            {
                component.moveDirection = newDirection;
                sparseMovement.SetComponentByEntity(entityId, component);
            }
        }

        public void SetMoveSpeedByEntity(int entityId, float newSpeed)
        {
            if (sparseMovement.TryGet(entityId, out MovementComponent component))
            {
                component.moveSpeed = newSpeed;
                sparseMovement.SetComponentByEntity(entityId, component);
            }
        }

        public void SetMoveInputByEntity(int entityId, int newInput)
        {
            if (sparseMovement.TryGet(entityId, out MovementComponent component))
            {
                component.moveInput = newInput;
                sparseMovement.SetComponentByEntity(entityId, component);
            }
        }

        public void SetMoveIntentByEntity(int entityId, Vector3 newDirection, int newInput)
        {
            if (sparseMovement.TryGet(entityId, out MovementComponent component))
            {
                component.moveDirection = newDirection;
                component.moveInput = newInput;
                sparseMovement.SetComponentByEntity(entityId, component);
            }
        }

        // -------------------------- Clear ---------------------
        public void ClearSystem()
        {
            Debug.Log("ClearMovementSystem");
            sparseMovement.Clear();
        }

    }
}
