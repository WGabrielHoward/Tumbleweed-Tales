
using UnityEngine;


public enum Effect
{
    unnassigned,
    none,
    damage,
    burn,
    freeze,
    poison,
    heal
    
}

public class EffectScript : MonoBehaviour
{
    [SerializeField] private Effect thisEffect;

    void Start()
    {
        if (thisEffect==Effect.unnassigned)
        {
            thisEffect = Effect.none;
        }
    }

    public virtual Effect GetEffect()
    {
        return thisEffect;
    }

    public virtual void SetEffect(Effect newEffect)
    {
        thisEffect = newEffect;
    }

}

