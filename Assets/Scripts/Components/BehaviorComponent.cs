using Scripts.Data;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Scripts.Components
{
    public struct BehaviorComponent
    {
        public Transform target;
        public Transform self;

        
        public float perceptionRange;    // rename to perceptionRange
        public NPCIntent intent;
        public NPCType type;
    }
}
