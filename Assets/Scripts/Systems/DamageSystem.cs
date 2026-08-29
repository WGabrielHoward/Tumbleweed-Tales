
using Scripts.Components;

using Scripts.Entities_Sets;

namespace Scripts.Systems
{
    public class DamageSystem
    {

        public static SparseSet<DamageComponent> sparseDamage = new SparseSet<DamageComponent>();


        public DamageSystem()
        {
            Launcher.Instance.LevelManager.OnLevelChange += ClearSystem;
            
        }

        // ---------------------- Registration ----------------------

        public void Register(int entityId, DamageComponent damage)
        {
            sparseDamage.Add(entityId, damage);
        }

        public void Unregister(int entityId)
        {
            sparseDamage.Remove(entityId);
        }


        public int GetDamage(int entityId)
        {
            DamageComponent damage;
            if (!sparseDamage.TryGet(entityId, out damage)) return 0;
            return damage.DamageAmount;
        }

        public void ClearSystem()
        {
            sparseDamage.Clear();
        }
    }
}
