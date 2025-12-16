
using UnityEngine;

using Scripts.NPC;

namespace Scripts.Player
{
    public class PlayerColliderAndTrigger : MonoBehaviour
    {

        private PlayerEffects playerEffects;
        private PlayerScriptManager playSMan;

        private void Awake()
        {
            playSMan = gameObject.GetComponent<PlayerScriptManager>();
            
        }
        private void Start()
        {
            playerEffects = playSMan.GetPlayerEffects();
        }

        public void SetEffectScript(PlayerEffects effectScript)
        {
            playerEffects = effectScript;
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, true);
                collision.gameObject.GetComponent<DamageOverTime>().StartPlayerDamage(this.gameObject);
            }
            if (collision.gameObject.CompareTag("Victory"))
            {
                LevelManager.ManInstance.Victory();
            }
            if (collision.gameObject.CompareTag("Effect"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, true);
                collision.gameObject.GetComponent<DamageOverTime>().StartPlayerDamage(this.gameObject);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, false);
                collision.gameObject.GetComponent<DamageOverTime>().StopPlayerDamage();
            }
            if (collision.gameObject.CompareTag("Effect"))
            {
                playerEffects.EffectsSwitch(collision.gameObject, false);
                collision.gameObject.GetComponent<DamageOverTime>().StopPlayerDamage();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                playerEffects.EffectsSwitch(other.gameObject, true);
                other.gameObject.GetComponent<DamageOverTime>().StartPlayerDamage(this.gameObject);
            }
            if (other.gameObject.CompareTag("Victory"))
            {
                LevelManager.ManInstance.Victory();
            }
            if (other.gameObject.CompareTag("Effect"))
            {
                playerEffects.EffectsSwitch(other.gameObject, true);
                other.gameObject.GetComponent<DamageOverTime>().StartPlayerDamage(this.gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
           
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                playerEffects.EffectsSwitch(other.gameObject, false);
                other.gameObject.GetComponent<DamageOverTime>().StopPlayerDamage();
            }
            if (other.gameObject.CompareTag("Effect"))
            {
                playerEffects.EffectsSwitch(other.gameObject, false);
                other.gameObject.GetComponent<DamageOverTime>().StopPlayerDamage();
            }
        }

    }
}