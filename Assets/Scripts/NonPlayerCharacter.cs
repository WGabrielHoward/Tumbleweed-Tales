using Unity.VisualScripting;
using UnityEngine;

public enum Effect
{
    burn,
    freeze,
    poison,
    heal,
    unnassigned
}

public class NonPlayerCharacter : MonoBehaviour
{
    private Rigidbody rbThis;
    [SerializeField] private GameObject target;
    [SerializeField] protected float forceToTarget = 1;
    //public virtual Effect effectType;
    [SerializeField] protected int health = 10;
    [SerializeField] protected int damage = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbThis = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (target.transform.position - transform.position).normalized;
        rbThis.AddForce(lookDirection * forceToTarget);
        if (transform.position.y < -10)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public virtual Effect GetEffect()
    {
        return Effect.unnassigned;
    }

    public virtual int GetDamage()
    {
        return damage;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

}
