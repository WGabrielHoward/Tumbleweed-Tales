using Scripts.Data;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Scripts.Entities_Sets
{
    public class SparseSet<TComponent> where TComponent: struct
    {
        private List<int> entities = new();
        private List<TComponent> components = new();
        private Dictionary<int, int> sparse = new();


        // Add
        public void Add(int entityId, TComponent component)
        {
            if (sparse.ContainsKey(entityId))
            {
                throw new InvalidOperationException($"Entity {entityId} already has a component of type {typeof(TComponent).Name}.");
            }

            int index = entities.Count;

            entities.Add(entityId);
            components.Add(component);

            sparse.Add(entityId, index);
        }
            
        // Remove
        public bool Remove(int entityId)
        {
            if (!sparse.TryGetValue(entityId, out int index))
                return false;

            int lastIndex = entities.Count - 1;

            // If we're not removing the last element,
            // move the last one into the gap
            if (index != lastIndex)
            {
                entities[index] = entities[lastIndex];
                components[index] = components[lastIndex];

                sparse[entities[index]] = index;
            }

            entities.RemoveAt(lastIndex);
            components.RemoveAt(lastIndex);
            sparse.Remove(entityId);

            return true;

        }

        // GetComponent
        public TComponent GetComponentByEntity(int entityId)
        {
            if (!sparse.TryGetValue(entityId, out int index))
            {
                throw new InvalidOperationException($"Entity {entityId} does not have a component of type {typeof(TComponent).Name}.");
            }
            return  components[index];
        }

        // SetComponent
        public void SetComponentByEntity(int entityId, TComponent component)
        {
            if (!sparse.TryGetValue(entityId, out int index))
            {
                throw new InvalidOperationException($"Entity {entityId} does not have a component of type {typeof(TComponent).Name}.");
            }
            components[index] = component;
        }

        public bool TryGet(int entityId, out TComponent component)
        {
            if (sparse.TryGetValue(entityId, out int index))
            {
                component = components[index];
                return true;
            }

            component = default;
            return false;
        }

        public bool Contains(int entityId)
        {
            return sparse.ContainsKey(entityId);
        }

        public int GetEntityByIndex(int index)
        {
            return entities[index];
        }

        public TComponent GetComponentByIndex(int index)
        {
            return components[index];
        }

        public void SetComponentByIndex(int index, TComponent component)
        {
            if (!sparse.ContainsValue(index))
            {
                throw new InvalidOperationException($"Index {index} not within sparseSet of type {typeof(TComponent).Name}.");
            }
            components[index] = component;
        }

        public void Clear()
        {
            entities.Clear();
            components.Clear();
            sparse.Clear();
        }

        public int Count => components.Count;

        
    }
}
