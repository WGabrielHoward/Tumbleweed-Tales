
using UnityEngine;

using Scripts.NPC;

namespace Scripts.Player
{
    public class PlayerScriptManager : MonoBehaviour
    {
        private Rigidbody rbPlayer;
        [Header("Player Settings")]
        [SerializeField] private float defaultForwardSpeed = 5;
        [SerializeField] private int defaultHealth = 100;
        [SerializeField] private GameObject focalPoint;

        private PlayerController playerController;
        private PlayerColliderAndTrigger playerColTrig;
        private PlayerEffects playerEffects;
        private PlayerStats playerStats;


        private void Awake()
        {
            playerController =  gameObject.AddComponent<PlayerController>();
            playerColTrig =     gameObject.AddComponent<PlayerColliderAndTrigger>();
            playerEffects =     gameObject.AddComponent<PlayerEffects>();
            playerStats =       gameObject.AddComponent<PlayerStats>();
            rbPlayer =          this.gameObject.GetComponent<Rigidbody>();
            focalPoint =        GameObject.Find("FocalPoint");
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //PushPlayerInfo();
        }

        void PushPlayerInfo()
        {
            playerStats.SetForwardSpeed(defaultForwardSpeed);
            // other info
        }

        // ------------------------------
        // Getters & Setters
        // ------------------------------
        public float GetForwardSpeed()
        {
            return defaultForwardSpeed;
        }

        public int GetHealth()
        {
            return defaultHealth;
        }

        public GameObject GetFocalPoint()
        {
            return focalPoint;
        }

        public Rigidbody GetRigidbody()
        {
            return rbPlayer;
        }

        public PlayerController GetPlayerController()
        {
            return playerController;
        }

        public PlayerColliderAndTrigger GetPlayerColTrig()
        {
            return playerColTrig;
        }

        public PlayerEffects GetPlayerEffects()
        {
            return playerEffects;
        }
        public PlayerStats GetPlayerStats()
        {
            return playerStats;
        }
    }
}