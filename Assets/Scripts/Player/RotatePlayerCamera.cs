using UnityEngine;

namespace Scripts.Player
{
    public class RotatePlayerCamera : MonoBehaviour
    {

        [SerializeField] private float rotationSpeed=200;
        private GameObject focalPoint;

        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (focalPoint == null)
            {
                focalPoint = GameObject.Find("Player");
            }
        }

        public void Rotate(float amount)
        {
            transform.position = focalPoint.transform.position;
            transform.Rotate(Vector3.up, amount * rotationSpeed * Time.deltaTime);
        }
        public Vector3 GetForward()
        {
            return transform.forward;
        }
    }
}