
using UnityEngine;

using Scripts.NPC;

namespace Scripts.Player
{
    public class PlayerColliderAndTrigger : MonoBehaviour
    {

        private PlayerEffects playerEffects;

        private void Start()
        {
            playerEffects = gameObject.GetComponent<PlayerEffects>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, true);
            }
            if (collision.gameObject.CompareTag("Victory"))
            {
                LevelManager.ManInstance.Victory();
            }
            if (collision.gameObject.CompareTag("Effect"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, true);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                int damage = collision.gameObject.GetComponent<NonPlayerCharacter>().GetDamage();
                this.GetComponent<PlayerStats>().Damage(damage);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, false);
            }
            if (collision.gameObject.CompareTag("Effect"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                playerEffects.EffectsSwitch(other.gameObject, true);
            }
            if (other.gameObject.CompareTag("Victory"))
            {
                LevelManager.ManInstance.Victory();
            }
            if (other.gameObject.CompareTag("Effect"))
            {
                playerEffects.EffectsSwitch(other.gameObject, true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                int damage = other.gameObject.GetComponent<NonPlayerCharacter>().GetDamage();
                this.GetComponent<PlayerStats>().Damage(damage);
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                playerEffects.EffectsSwitch(other.gameObject, false);
            }
            if (other.gameObject.CompareTag("Effect"))
            {
                playerEffects.EffectsSwitch(other.gameObject, false);
            }
        }

    }
}