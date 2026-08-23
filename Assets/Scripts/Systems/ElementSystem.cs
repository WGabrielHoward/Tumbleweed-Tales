using System.Collections.Generic;
using UnityEngine;
using Scripts.Components;

using Scripts.Data;
using Scripts.Entities_Sets;

namespace Scripts.Systems
{
    public class ElementSystem 
    {

        public static SparseSet<ElementComponent> sparseElement = new SparseSet<ElementComponent>();



        public ElementSystem()
        {
            Launcher.Instance.LevelManager.OnLevelChange += ClearSystem;
        }

        // ---------------------- Registration ----------------------

        public void Register(int entityId, ElementComponent element)
        {
            sparseElement.Add(entityId, element);
        }

        public void Unregister(int entityId)
        {
            sparseElement.Remove(entityId);
        }


        public Element GetElement(int entityId)
        {
            ElementComponent element;
            if (!sparseElement.TryGet(entityId, out element)) return 0;
            return element.elementType;
        }


        public void ClearSystem()
        {
            sparseElement.Clear();
        }


    }
}
