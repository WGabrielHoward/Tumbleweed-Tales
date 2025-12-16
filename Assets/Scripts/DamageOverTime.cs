
using System.Collections;
using UnityEngine;

using Scripts.Player;
using Scripts.NPC;

public class DamageOverTime : MonoBehaviour
{

    [SerializeField] protected int damagePerTick = 0;
    [SerializeField] protected float tickRate = 1f;
    private bool damagePlayer;
    private bool damageNPC;
    
    public void StartPlayerDamage(GameObject objToDamage)
    {
        damagePlayer = true;
        StartCoroutine(ConstantPlayerDamage(objToDamage));
    }
    
    public void StopPlayerDamage()
    {
        damagePlayer = false;
    }

    IEnumerator ConstantPlayerDamage(GameObject objToDamage)
    {
        while (damagePlayer)
        {
            yield return new WaitForSeconds(tickRate);
            objToDamage.GetComponent<PlayerStats>().Damage(damagePerTick);

        }
    }

    public void StartNPCDamage(GameObject objToDamage)
    {
        damageNPC = true;
        StartCoroutine(ConstantNPCDamage(objToDamage));
    }

    public void StopNPCDamage()
    {
        damagePlayer = false;
    }

    IEnumerator ConstantNPCDamage(GameObject objToDamage)
    {
        while (damageNPC)
        {
            yield return new WaitForSeconds(tickRate);
            objToDamage.GetComponent<NonPlayerCharacter>().TakeDamage(damagePerTick);

        }
    }

    public void SetDamagePerTick(int damage)
    {
        damagePerTick = damage;
    }
    public void SetTickRate(float rate)
    {
        tickRate = rate;
    }

}


