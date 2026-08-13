using UnityEngine;
using Scripts.Components;

    public static class ElementRules
    {
        public static Effect GetStatusForElement(Element element)
        {
        return element switch
        {
            Element.Fire => Effect.Burn,
            Element.Ice => Effect.Freeze,
            Element.Poison => Effect.Poison,
            _ => Effect.None,

        };
        }

        public static int CalculateDamage(int baseDamage, Element attackElement, Element defendElement)
        {
            float multiplier = DamageMultiplier[(int)attackElement, (int)defendElement];
            int finalDamage = Mathf.RoundToInt(baseDamage*multiplier);
            return finalDamage;
        }

            public static readonly float[,] DamageMultiplier =
        {
            // [Attack]|[Defend]| None Fire Ice Poison 
            /*None*/             {1,   1,   1,   1 },           
            /*Fire*/             {1,   1,   2,   1 },           // fire deals double damage to ice
            /*Ice*/              {1,  2f,   1,   1 },           // ice deals double damage to fire
            /*Poison*/           {2,  0.5f, 0.5f,1 }            // poison deals double damage to none, half to fire and ice
           
        };
}

