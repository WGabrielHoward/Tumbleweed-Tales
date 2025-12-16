using System.Collections;
using UnityEngine;

using Scripts.NPC;

namespace Scripts.Player
{
    public class PlayerStats : MonoBehaviour
    {
        private PlayerScriptManager playSMan;
        private PlayerController playerController;

        [Header("Player Stats")]
        [SerializeField] private float forwardSpeed;
        [SerializeField] private int health;

        LevelCanvas levelCanvas;

        private void Awake()
        {
            playSMan = gameObject.GetComponent<PlayerScriptManager>();
            
        }
        private void Start()
        {
            levelCanvas = FindAnyObjectByType<LevelCanvas>();
            playerController = playSMan.GetPlayerController();
            PullForwardSpeed();
            PullHealth();
        }

        public float GetForwardSpeed()
        {
            return forwardSpeed;
        }

        public void SetForwardSpeed(float newSpeed)
        {
            forwardSpeed = newSpeed;
            playerController.SetForwardSpeed(forwardSpeed);
        }

        private void PullForwardSpeed()
        {
            forwardSpeed = playSMan.GetForwardSpeed();
        }
        private void PullHealth()
        {
            health = playSMan.GetHealth();
        }


        public void Damage(int damage)
        {
            health -= damage;
            UpdateHealthText();
            if (health <= 0)
            {
                LevelManager.ManInstance.GameOver();
            }
        }

        private void UpdateHealthText()
        {
            if (levelCanvas)
            {
                levelCanvas.HealthUpdate(health);
            }
        }

    }
}