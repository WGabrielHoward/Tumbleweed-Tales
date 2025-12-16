
using UnityEngine;

namespace Scripts.NPC
{
  
    public class NonPlayerCharacter : MonoBehaviour
    {
        protected Rigidbody rbThis;
        [SerializeField] public GameObject target;
        [SerializeField] protected float forceToTarget = 1;
        //public virtual Effect effectType;
        [SerializeField] protected int health = 10;
        [SerializeField] protected int damage = 0;
        public EffectScript thisEffect; // How can I add this as a component so that other scripts can just get it outside of NPC?

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Start()
        {
            rbThis = gameObject.GetComponent<Rigidbody>();
            thisEffect = gameObject.AddComponent<EffectScript>();

        }

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                Vector3 lookDirection = (target.transform.position - transform.position).normalized;
                rbThis.AddForce(lookDirection * forceToTarget);
            }

            if (transform.position.y < -10)
            {
                Death();
            }
        }

        public virtual void SetTarget(GameObject newTarget)
        {
            if (newTarget == null)
            {
                target = GameObject.Find("Player");
            }
            else target = newTarget;

        }


        protected virtual void Death()
        {
            Destroy(gameObject);
        }

        public virtual Effect GetEffect()
        {
            return thisEffect.GetEffect();
        }
        public virtual void SetEffect(Effect newEffect)
        {
            thisEffect.SetEffect(newEffect);
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

}
